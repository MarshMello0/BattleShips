using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0.15,0.15,0.15,0.15,0]")]
	public partial class PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 4;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		private Quaternion _FLCannon;
		public event FieldEvent<Quaternion> FLCannonChanged;
		public InterpolateQuaternion FLCannonInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion FLCannon
		{
			get { return _FLCannon; }
			set
			{
				// Don't do anything if the value is the same
				if (_FLCannon == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_FLCannon = value;
				hasDirtyFields = true;
			}
		}

		public void SetFLCannonDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_FLCannon(ulong timestep)
		{
			if (FLCannonChanged != null) FLCannonChanged(_FLCannon, timestep);
			if (fieldAltered != null) fieldAltered("FLCannon", _FLCannon, timestep);
		}
		private Quaternion _FRCannon;
		public event FieldEvent<Quaternion> FRCannonChanged;
		public InterpolateQuaternion FRCannonInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion FRCannon
		{
			get { return _FRCannon; }
			set
			{
				// Don't do anything if the value is the same
				if (_FRCannon == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_FRCannon = value;
				hasDirtyFields = true;
			}
		}

		public void SetFRCannonDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_FRCannon(ulong timestep)
		{
			if (FRCannonChanged != null) FRCannonChanged(_FRCannon, timestep);
			if (fieldAltered != null) fieldAltered("FRCannon", _FRCannon, timestep);
		}
		private Quaternion _BLCannon;
		public event FieldEvent<Quaternion> BLCannonChanged;
		public InterpolateQuaternion BLCannonInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion BLCannon
		{
			get { return _BLCannon; }
			set
			{
				// Don't do anything if the value is the same
				if (_BLCannon == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_BLCannon = value;
				hasDirtyFields = true;
			}
		}

		public void SetBLCannonDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_BLCannon(ulong timestep)
		{
			if (BLCannonChanged != null) BLCannonChanged(_BLCannon, timestep);
			if (fieldAltered != null) fieldAltered("BLCannon", _BLCannon, timestep);
		}
		private Quaternion _BRCannon;
		public event FieldEvent<Quaternion> BRCannonChanged;
		public InterpolateQuaternion BRCannonInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion BRCannon
		{
			get { return _BRCannon; }
			set
			{
				// Don't do anything if the value is the same
				if (_BRCannon == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_BRCannon = value;
				hasDirtyFields = true;
			}
		}

		public void SetBRCannonDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_BRCannon(ulong timestep)
		{
			if (BRCannonChanged != null) BRCannonChanged(_BRCannon, timestep);
			if (fieldAltered != null) fieldAltered("BRCannon", _BRCannon, timestep);
		}
		private int _health;
		public event FieldEvent<int> healthChanged;
		public Interpolated<int> healthInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int health
		{
			get { return _health; }
			set
			{
				// Don't do anything if the value is the same
				if (_health == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x40;
				_health = value;
				hasDirtyFields = true;
			}
		}

		public void SethealthDirty()
		{
			_dirtyFields[0] |= 0x40;
			hasDirtyFields = true;
		}

		private void RunChange_health(ulong timestep)
		{
			if (healthChanged != null) healthChanged(_health, timestep);
			if (fieldAltered != null) fieldAltered("health", _health, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			FLCannonInterpolation.current = FLCannonInterpolation.target;
			FRCannonInterpolation.current = FRCannonInterpolation.target;
			BLCannonInterpolation.current = BLCannonInterpolation.target;
			BRCannonInterpolation.current = BRCannonInterpolation.target;
			healthInterpolation.current = healthInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _FLCannon);
			UnityObjectMapper.Instance.MapBytes(data, _FRCannon);
			UnityObjectMapper.Instance.MapBytes(data, _BLCannon);
			UnityObjectMapper.Instance.MapBytes(data, _BRCannon);
			UnityObjectMapper.Instance.MapBytes(data, _health);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_FLCannon = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			FLCannonInterpolation.current = _FLCannon;
			FLCannonInterpolation.target = _FLCannon;
			RunChange_FLCannon(timestep);
			_FRCannon = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			FRCannonInterpolation.current = _FRCannon;
			FRCannonInterpolation.target = _FRCannon;
			RunChange_FRCannon(timestep);
			_BLCannon = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			BLCannonInterpolation.current = _BLCannon;
			BLCannonInterpolation.target = _BLCannon;
			RunChange_BLCannon(timestep);
			_BRCannon = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			BRCannonInterpolation.current = _BRCannon;
			BRCannonInterpolation.target = _BRCannon;
			RunChange_BRCannon(timestep);
			_health = UnityObjectMapper.Instance.Map<int>(payload);
			healthInterpolation.current = _health;
			healthInterpolation.target = _health;
			RunChange_health(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _FLCannon);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _FRCannon);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _BLCannon);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _BRCannon);
			if ((0x40 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _health);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (FLCannonInterpolation.Enabled)
				{
					FLCannonInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					FLCannonInterpolation.Timestep = timestep;
				}
				else
				{
					_FLCannon = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_FLCannon(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (FRCannonInterpolation.Enabled)
				{
					FRCannonInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					FRCannonInterpolation.Timestep = timestep;
				}
				else
				{
					_FRCannon = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_FRCannon(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (BLCannonInterpolation.Enabled)
				{
					BLCannonInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					BLCannonInterpolation.Timestep = timestep;
				}
				else
				{
					_BLCannon = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_BLCannon(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (BRCannonInterpolation.Enabled)
				{
					BRCannonInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					BRCannonInterpolation.Timestep = timestep;
				}
				else
				{
					_BRCannon = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_BRCannon(timestep);
				}
			}
			if ((0x40 & readDirtyFlags[0]) != 0)
			{
				if (healthInterpolation.Enabled)
				{
					healthInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					healthInterpolation.Timestep = timestep;
				}
				else
				{
					_health = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_health(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (FLCannonInterpolation.Enabled && !FLCannonInterpolation.current.UnityNear(FLCannonInterpolation.target, 0.0015f))
			{
				_FLCannon = (Quaternion)FLCannonInterpolation.Interpolate();
				//RunChange_FLCannon(FLCannonInterpolation.Timestep);
			}
			if (FRCannonInterpolation.Enabled && !FRCannonInterpolation.current.UnityNear(FRCannonInterpolation.target, 0.0015f))
			{
				_FRCannon = (Quaternion)FRCannonInterpolation.Interpolate();
				//RunChange_FRCannon(FRCannonInterpolation.Timestep);
			}
			if (BLCannonInterpolation.Enabled && !BLCannonInterpolation.current.UnityNear(BLCannonInterpolation.target, 0.0015f))
			{
				_BLCannon = (Quaternion)BLCannonInterpolation.Interpolate();
				//RunChange_BLCannon(BLCannonInterpolation.Timestep);
			}
			if (BRCannonInterpolation.Enabled && !BRCannonInterpolation.current.UnityNear(BRCannonInterpolation.target, 0.0015f))
			{
				_BRCannon = (Quaternion)BRCannonInterpolation.Interpolate();
				//RunChange_BRCannon(BRCannonInterpolation.Timestep);
			}
			if (healthInterpolation.Enabled && !healthInterpolation.current.UnityNear(healthInterpolation.target, 0.0015f))
			{
				_health = (int)healthInterpolation.Interpolate();
				//RunChange_health(healthInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerNetworkObject() : base() { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
