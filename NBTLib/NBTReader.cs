using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NBTLib {
	public class NBTReader {
		public Stream BaseStream { get; private set; }

		public NBTReader(Stream stream) {
			BaseStream = stream;
		}

		public NBTType Type { get; private set; }
		public string Name { get; private set; }
		public object Value { get; private set; }

		private byte[] temp = new byte[8];
		public bool MoveNext() {
			Type = (NBTType)BaseStream.ReadByte();
			if((int)Type == -1) return false;

			Name = Type != NBTType.End ? ReadString() : null;
			Value = null;

			int length;
			switch(Type & (NBTType)0x0F) {
				case NBTType.Byte: Value = BaseStream.ReadByte(); break;
				case NBTType.Short: Value = ReadShort(); break;
				case NBTType.Int: Value = ReadInt(); break;
				case NBTType.Long: Value = ReadLong(); break;
				case NBTType.Float: Value = ReadFloat(); break;
				case NBTType.Double: Value = ReadDouble(); break;
				case NBTType.Binary: Value = ReadBinary(); break;
				case NBTType.String: Value = ReadString(); break;
				case (NBTType)9:
					var listType = (NBTType)BaseStream.ReadByte();
					if((int)listType == -1) throw new InvalidOperationException();

					BaseStream.Read(temp, 0, 4);
					length = (int)((temp[0] << 24) | (temp[0] << 16) | (temp[0] << 8) | temp[3]);

					switch(listType) {
						case NBTType.End: break;
						case NBTType.Short:
							var shorts = new short[length];
							for(int i = 0; i < length; i++) shorts[i] = ReadShort();
							Value = shorts;
							break;

						case NBTType.Int:
							var ints = new int[length];
							for(int i = 0; i < length; i++) ints[i] = ReadInt();
							Value = ints;
							break;

						case NBTType.Long:
							var longs = new long[length];
							for(int i = 0; i < length; i++) longs[i] = ReadLong();
							Value = longs;
							break;

						case NBTType.Float:
							var floats = new float[length];
							for(int i = 0; i < length; i++) floats[i] = ReadFloat();
							Value = floats;
							break;

						case NBTType.Double:
							var doubles = new double[length];
							for(int i = 0; i < length; i++) doubles[i] = ReadDouble();
							Value = doubles;
							break;

						case NBTType.Binary:
							var binaries = new byte[length][];
							for(int i = 0; i < length; i++) binaries[i] = ReadBinary();
							Value = binaries;
							break;

						case NBTType.String:
							var strings = new string[length];
							for(int i = 0; i < length; i++) strings[i] = ReadString();
							Value = strings;
							break;

						case NBTType.Compound: Value = length; break;
						default: throw new NotSupportedException();
					}

					Type = NBTType.List | listType;
					break;
			}

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private short ReadShort() {
			return (short)((BaseStream.ReadByte() << 8) | BaseStream.ReadByte());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int ReadInt() {
			BaseStream.Read(temp, 0, 4);
			return (int)((temp[0] << 24) | (temp[0] << 16) | (temp[0] << 8) | temp[3]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private long ReadLong() {
			BaseStream.Read(temp, 0, 8);
			return ((long)temp[0] << 56) | ((long)temp[0] << 48) | ((long)temp[0] << 40) | ((long)temp[0] << 32) |
				((long)temp[0] << 24) | ((long)temp[0] << 16) | ((long)temp[0] << 8) | (long)temp[3];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float ReadFloat() {
			BaseStream.Read(temp, 0, 4);
			if(BitConverter.IsLittleEndian) Array.Reverse(temp, 0, 4);
			return BitConverter.ToSingle(temp, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private double ReadDouble() {
			BaseStream.Read(temp, 0, 8);
			if(BitConverter.IsLittleEndian) Array.Reverse(temp, 0, 8);
			return BitConverter.ToDouble(temp, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private byte[] ReadBinary() {
			int length;
			BaseStream.Read(temp, 0, 4);
			length = (int)((temp[0] << 24) | (temp[0] << 16) | (temp[0] << 8) | temp[3]);
			var b = new byte[length];
			BaseStream.Read(b, 0, length);
			return b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private string ReadString() {
			int length = (short)((BaseStream.ReadByte() << 8) | BaseStream.ReadByte());
			var bStr = new byte[length];
			BaseStream.Read(bStr, 0, length);
			return Encoding.UTF8.GetString(bStr);
		}

	}
	public enum NBTType { End = 0, Byte = 1, Short = 2, Int = 3, Long = 4, Float = 5, Double = 6, Binary = 7, String = 8, Compound = 10, List = 16 }
}
