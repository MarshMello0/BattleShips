using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[\"uint\", \"string\"][\"uint\", \"int\"][\"uint\", \"string\", \"int\"][\"uint\"]]")]
	[GeneratedRPCVariableNames("{\"types\":[[\"id\", \"name\"][\"id\", \"newScore\"][\"id\", \"name\", \"score\"][\"id\"]]")]
	public abstract partial class LeaderboardBehavior : NetworkBehavior
	{
		public const byte RPC_ADD_PLAYER = 0 + 5;
		public const byte RPC_UPDATE_SCORE = 1 + 5;
		public const byte RPC_ADD_PLAYER_WITH_SCORE = 2 + 5;
		public const byte RPC_REMOVE_PLAYER = 3 + 5;
		
		public LeaderboardNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (LeaderboardNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("AddPlayer", AddPlayer, typeof(uint), typeof(string));
			networkObject.RegisterRpc("UpdateScore", UpdateScore, typeof(uint), typeof(int));
			networkObject.RegisterRpc("AddPlayerWithScore", AddPlayerWithScore, typeof(uint), typeof(string), typeof(int));
			networkObject.RegisterRpc("RemovePlayer", RemovePlayer, typeof(uint));

			networkObject.onDestroy += DestroyGameObject;

			if (!obj.IsOwner)
			{
				if (!skipAttachIds.ContainsKey(obj.NetworkId)){
					uint newId = obj.NetworkId + 1;
					ProcessOthers(gameObject.transform, ref newId);
				}
				else
					skipAttachIds.Remove(obj.NetworkId);
			}

			if (obj.Metadata != null)
			{
				byte transformFlags = obj.Metadata[0];

				if (transformFlags != 0)
				{
					BMSByte metadataTransform = new BMSByte();
					metadataTransform.Clone(obj.Metadata);
					metadataTransform.MoveStartIndex(1);

					if ((transformFlags & 0x01) != 0 && (transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() =>
						{
							transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform);
							transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform);
						});
					}
					else if ((transformFlags & 0x01) != 0)
					{
						MainThreadManager.Run(() => { transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform); });
					}
					else if ((transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() => { transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform); });
					}
				}
			}

			MainThreadManager.Run(() =>
			{
				NetworkStart();
				networkObject.Networker.FlushCreateActions(networkObject);
			});
		}

		protected override void CompleteRegistration()
		{
			base.CompleteRegistration();
			networkObject.ReleaseCreateBuffer();
		}

		public override void Initialize(NetWorker networker, byte[] metadata = null)
		{
			Initialize(new LeaderboardNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new LeaderboardNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// uint id
		/// string name
		/// </summary>
		public abstract void AddPlayer(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// uint id
		/// int newScore
		/// </summary>
		public abstract void UpdateScore(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// uint id
		/// string name
		/// int score
		/// </summary>
		public abstract void AddPlayerWithScore(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// uint id
		/// </summary>
		public abstract void RemovePlayer(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}