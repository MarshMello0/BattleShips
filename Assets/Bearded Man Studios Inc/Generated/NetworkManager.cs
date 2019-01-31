using BeardedManStudios.Forge.Networking.Generated;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Unity
{
	public partial class NetworkManager : MonoBehaviour
	{
		public delegate void InstantiateEvent(INetworkBehavior unityGameObject, NetworkObject obj);
		public event InstantiateEvent objectInitialized;
		private BMSByte metadata = new BMSByte();

		public GameObject[] CannonBallNetworkObject = null;
		public GameObject[] ChatNetworkObject = null;
		public GameObject[] LeaderboardNetworkObject = null;
		public GameObject[] PlayerNetworkObject = null;

		private void SetupObjectCreatedEvent()
		{
			Networker.objectCreated += CaptureObjects;
		}

		private void OnDestroy()
		{
		    if (Networker != null)
				Networker.objectCreated -= CaptureObjects;
		}
		
		private void CaptureObjects(NetworkObject obj)
		{
			if (obj.CreateCode < 0)
				return;
				
			if (obj is CannonBallNetworkObject)
			{
				MainThreadManager.Run(() =>
				{
					NetworkBehavior newObj = null;
					if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
					{
						if (CannonBallNetworkObject.Length > 0 && CannonBallNetworkObject[obj.CreateCode] != null)
						{
							var go = Instantiate(CannonBallNetworkObject[obj.CreateCode]);
							newObj = go.GetComponent<CannonBallBehavior>();
						}
					}

					if (newObj == null)
						return;
						
					newObj.Initialize(obj);

					if (objectInitialized != null)
						objectInitialized(newObj, obj);
				});
			}
			else if (obj is ChatNetworkObject)
			{
				MainThreadManager.Run(() =>
				{
					NetworkBehavior newObj = null;
					if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
					{
						if (ChatNetworkObject.Length > 0 && ChatNetworkObject[obj.CreateCode] != null)
						{
							var go = Instantiate(ChatNetworkObject[obj.CreateCode]);
							newObj = go.GetComponent<ChatBehavior>();
						}
					}

					if (newObj == null)
						return;
						
					newObj.Initialize(obj);

					if (objectInitialized != null)
						objectInitialized(newObj, obj);
				});
			}
			else if (obj is LeaderboardNetworkObject)
			{
				MainThreadManager.Run(() =>
				{
					NetworkBehavior newObj = null;
					if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
					{
						if (LeaderboardNetworkObject.Length > 0 && LeaderboardNetworkObject[obj.CreateCode] != null)
						{
							var go = Instantiate(LeaderboardNetworkObject[obj.CreateCode]);
							newObj = go.GetComponent<LeaderboardBehavior>();
						}
					}

					if (newObj == null)
						return;
						
					newObj.Initialize(obj);

					if (objectInitialized != null)
						objectInitialized(newObj, obj);
				});
			}
			else if (obj is PlayerNetworkObject)
			{
				MainThreadManager.Run(() =>
				{
					NetworkBehavior newObj = null;
					if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
					{
						if (PlayerNetworkObject.Length > 0 && PlayerNetworkObject[obj.CreateCode] != null)
						{
							var go = Instantiate(PlayerNetworkObject[obj.CreateCode]);
							newObj = go.GetComponent<PlayerBehavior>();
						}
					}

					if (newObj == null)
						return;
						
					newObj.Initialize(obj);

					if (objectInitialized != null)
						objectInitialized(newObj, obj);
				});
			}
		}

		private void InitializedObject(INetworkBehavior behavior, NetworkObject obj)
		{
			if (objectInitialized != null)
				objectInitialized(behavior, obj);

			obj.pendingInitialized -= InitializedObject;
		}

		[Obsolete("Use InstantiateCannonBall instead, its shorter and easier to type out ;)")]
		public CannonBallBehavior InstantiateCannonBallNetworkObject(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(CannonBallNetworkObject[index]);
			var netBehavior = go.GetComponent<CannonBallBehavior>();
			var obj = netBehavior.CreateNetworkObject(Networker, index);
			go.GetComponent<CannonBallBehavior>().networkObject = (CannonBallNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}
		[Obsolete("Use InstantiateChat instead, its shorter and easier to type out ;)")]
		public ChatBehavior InstantiateChatNetworkObject(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(ChatNetworkObject[index]);
			var netBehavior = go.GetComponent<ChatBehavior>();
			var obj = netBehavior.CreateNetworkObject(Networker, index);
			go.GetComponent<ChatBehavior>().networkObject = (ChatNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}
		[Obsolete("Use InstantiateLeaderboard instead, its shorter and easier to type out ;)")]
		public LeaderboardBehavior InstantiateLeaderboardNetworkObject(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(LeaderboardNetworkObject[index]);
			var netBehavior = go.GetComponent<LeaderboardBehavior>();
			var obj = netBehavior.CreateNetworkObject(Networker, index);
			go.GetComponent<LeaderboardBehavior>().networkObject = (LeaderboardNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}
		[Obsolete("Use InstantiatePlayer instead, its shorter and easier to type out ;)")]
		public PlayerBehavior InstantiatePlayerNetworkObject(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(PlayerNetworkObject[index]);
			var netBehavior = go.GetComponent<PlayerBehavior>();
			var obj = netBehavior.CreateNetworkObject(Networker, index);
			go.GetComponent<PlayerBehavior>().networkObject = (PlayerNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}

		/// <summary>
		/// Instantiate an instance of CannonBall
		/// </summary>
		/// <returns>
		/// A local instance of CannonBallBehavior
		/// </returns>
		/// <param name="index">The index of the CannonBall prefab in the NetworkManager to Instantiate</param>
		/// <param name="position">Optional parameter which defines the position of the created GameObject</param>
		/// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
		/// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
		public CannonBallBehavior InstantiateCannonBall(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(CannonBallNetworkObject[index]);
			var netBehavior = go.GetComponent<CannonBallBehavior>();

			NetworkObject obj = null;
			if (!sendTransform && position == null && rotation == null)
				obj = netBehavior.CreateNetworkObject(Networker, index);
			else
			{
				metadata.Clear();

				if (position == null && rotation == null)
				{
					byte transformFlags = 0x1 | 0x2;
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);
					ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
				}
				else
				{
					byte transformFlags = 0x0;
					transformFlags |= (byte)(position != null ? 0x1 : 0x0);
					transformFlags |= (byte)(rotation != null ? 0x2 : 0x0);
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);

					if (position != null)
						ObjectMapper.Instance.MapBytes(metadata, position.Value);

					if (rotation != null)
						ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
				}

				obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
			}

			go.GetComponent<CannonBallBehavior>().networkObject = (CannonBallNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}
		/// <summary>
		/// Instantiate an instance of Chat
		/// </summary>
		/// <returns>
		/// A local instance of ChatBehavior
		/// </returns>
		/// <param name="index">The index of the Chat prefab in the NetworkManager to Instantiate</param>
		/// <param name="position">Optional parameter which defines the position of the created GameObject</param>
		/// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
		/// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
		public ChatBehavior InstantiateChat(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(ChatNetworkObject[index]);
			var netBehavior = go.GetComponent<ChatBehavior>();

			NetworkObject obj = null;
			if (!sendTransform && position == null && rotation == null)
				obj = netBehavior.CreateNetworkObject(Networker, index);
			else
			{
				metadata.Clear();

				if (position == null && rotation == null)
				{
					byte transformFlags = 0x1 | 0x2;
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);
					ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
				}
				else
				{
					byte transformFlags = 0x0;
					transformFlags |= (byte)(position != null ? 0x1 : 0x0);
					transformFlags |= (byte)(rotation != null ? 0x2 : 0x0);
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);

					if (position != null)
						ObjectMapper.Instance.MapBytes(metadata, position.Value);

					if (rotation != null)
						ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
				}

				obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
			}

			go.GetComponent<ChatBehavior>().networkObject = (ChatNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}
		/// <summary>
		/// Instantiate an instance of Leaderboard
		/// </summary>
		/// <returns>
		/// A local instance of LeaderboardBehavior
		/// </returns>
		/// <param name="index">The index of the Leaderboard prefab in the NetworkManager to Instantiate</param>
		/// <param name="position">Optional parameter which defines the position of the created GameObject</param>
		/// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
		/// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
		public LeaderboardBehavior InstantiateLeaderboard(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(LeaderboardNetworkObject[index]);
			var netBehavior = go.GetComponent<LeaderboardBehavior>();

			NetworkObject obj = null;
			if (!sendTransform && position == null && rotation == null)
				obj = netBehavior.CreateNetworkObject(Networker, index);
			else
			{
				metadata.Clear();

				if (position == null && rotation == null)
				{
					byte transformFlags = 0x1 | 0x2;
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);
					ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
				}
				else
				{
					byte transformFlags = 0x0;
					transformFlags |= (byte)(position != null ? 0x1 : 0x0);
					transformFlags |= (byte)(rotation != null ? 0x2 : 0x0);
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);

					if (position != null)
						ObjectMapper.Instance.MapBytes(metadata, position.Value);

					if (rotation != null)
						ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
				}

				obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
			}

			go.GetComponent<LeaderboardBehavior>().networkObject = (LeaderboardNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}
		/// <summary>
		/// Instantiate an instance of Player
		/// </summary>
		/// <returns>
		/// A local instance of PlayerBehavior
		/// </returns>
		/// <param name="index">The index of the Player prefab in the NetworkManager to Instantiate</param>
		/// <param name="position">Optional parameter which defines the position of the created GameObject</param>
		/// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
		/// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
		public PlayerBehavior InstantiatePlayer(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(PlayerNetworkObject[index]);
			var netBehavior = go.GetComponent<PlayerBehavior>();

			NetworkObject obj = null;
			if (!sendTransform && position == null && rotation == null)
				obj = netBehavior.CreateNetworkObject(Networker, index);
			else
			{
				metadata.Clear();

				if (position == null && rotation == null)
				{
					byte transformFlags = 0x1 | 0x2;
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);
					ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
				}
				else
				{
					byte transformFlags = 0x0;
					transformFlags |= (byte)(position != null ? 0x1 : 0x0);
					transformFlags |= (byte)(rotation != null ? 0x2 : 0x0);
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);

					if (position != null)
						ObjectMapper.Instance.MapBytes(metadata, position.Value);

					if (rotation != null)
						ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
				}

				obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
			}

			go.GetComponent<PlayerBehavior>().networkObject = (PlayerNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}
	}
}
