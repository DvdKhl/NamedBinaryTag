using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NBTLib {
	public class NBTWriter {
		public Stream BaseStream { get; private set; }

		byte[] temp = new byte[8];

		public NBTWriter(Stream stream) {
			BaseStream = stream;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteValue(string value) {
			byte[] b = Encoding.UTF8.GetBytes(value);
			byte[] length = BitConverter.GetBytes((short)b.Length);

			if(BitConverter.IsLittleEndian) Array.Reverse(length);
			BaseStream.Write(length, 0, 2);
			BaseStream.Write(b, 0, b.Length);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteValue(short value) {
			temp[0] = (byte)((value >> 8) & 0xFF);
			temp[1] = (byte)((value >> 0) & 0xFF);
			BaseStream.Write(temp, 0, 2);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteValue(int value) {
			temp[0] = (byte)((value >> 24) & 0xFF);
			temp[1] = (byte)((value >> 16) & 0xFF);
			temp[2] = (byte)((value >> 8) & 0xFF);
			temp[3] = (byte)((value >> 0) & 0xFF);
			BaseStream.Write(temp, 0, 4);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteValue(long value) {
			temp[0] = (byte)((value >> 56) & 0xFF);
			temp[1] = (byte)((value >> 48) & 0xFF);
			temp[2] = (byte)((value >> 40) & 0xFF);
			temp[3] = (byte)((value >> 32) & 0xFF);
			temp[4] = (byte)((value >> 24) & 0xFF);
			temp[5] = (byte)((value >> 16) & 0xFF);
			temp[6] = (byte)((value >> 8) & 0xFF);
			temp[7] = (byte)((value >> 0) & 0xFF);
			BaseStream.Write(temp, 0, 8);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteListHeader(string name, byte type, int length) {
			BaseStream.WriteByte(9);
			WriteValue(name);
			BaseStream.WriteByte(type);

			byte[] b = BitConverter.GetBytes(length);
			if(BitConverter.IsLittleEndian) Array.Reverse(b);
			BaseStream.Write(b, 0, 4);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteValue(float value) {
			byte[] b = BitConverter.GetBytes(value);
			if(BitConverter.IsLittleEndian) Array.Reverse(b);
			BaseStream.Write(b, 0, 4);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteValue(double value) {
			byte[] b = BitConverter.GetBytes(value);
			if(BitConverter.IsLittleEndian) Array.Reverse(b);
			BaseStream.Write(b, 0, 8);
		}

		public void Write(string name, byte value) {
			BaseStream.WriteByte(1);
			WriteValue(name);
			BaseStream.WriteByte(value);
		}
		public void Write(string name, short value) {
			BaseStream.WriteByte(2);
			WriteValue(name);
			WriteValue(value);
		}
		public void Write(string name, int value) {
			BaseStream.WriteByte(3);
			WriteValue(name);
			WriteValue(value);
		}
		public void Write(string name, long value) {
			BaseStream.WriteByte(4);
			WriteValue(name);
			WriteValue(value);
		}
		public void Write(string name, float value) {
			BaseStream.WriteByte(5);
			WriteValue(name);
			WriteValue(value);
		}
		public void Write(string name, double value) {
			BaseStream.WriteByte(6);
			WriteValue(name);
			WriteValue(value);
		}
		public void Write(string name, byte[] value) {
			BaseStream.WriteByte(7);
			WriteValue(name);

			byte[] b = BitConverter.GetBytes(value.Length);
			if(BitConverter.IsLittleEndian) Array.Reverse(b);
			BaseStream.Write(b, 0, 4);

			BaseStream.Write(value, 0, value.Length);
		}
		public void Write(string name, string value) {
			BaseStream.WriteByte(8);
			WriteValue(name);
			WriteValue(value);
		}
		public void Write(string name, short[] values) {
			WriteListHeader(name, 2, values.Length);
			foreach(var value in values) WriteValue(value);
		}
		public void Write(string name, int[] values) {
			BaseStream.WriteByte(11);
			WriteValue(name);

			byte[] b = BitConverter.GetBytes(values.Length);
			if(BitConverter.IsLittleEndian) Array.Reverse(b);
			BaseStream.Write(b, 0, 4);

			foreach(var value in values) WriteValue(value);
		}
		public void Write(string name, long[] values) {
			WriteListHeader(name, 4, values.Length);
			foreach(var value in values) WriteValue(value);
		}
		public void Write(string name, float[] values) {
			WriteListHeader(name, 4, values.Length);
			foreach(var value in values) WriteValue(value);
		}
		public void Write(string name, double[] values) {
			WriteListHeader(name, 4, values.Length);
			foreach(var value in values) WriteValue(value);
		}
		public void Write(string name, string[] values) {
			WriteListHeader(name, 4, values.Length);
			foreach(var value in values) WriteValue(value);
		}
		public void Write(string name, int length, Action<NBTWriter, int> onCompound) {
			WriteListHeader(name, 10, length);
			for(int i = 0; i < length; i++) {
				onCompound(this, i);
				BaseStream.WriteByte(0);
			}
		}
		public void Write<T>(string name, T[] items, Action<NBTWriter, T> onCompound) {
			WriteListHeader(name, 10, items.Length);
			for(int i = 0; i < items.Length; i++) {
				onCompound(this, items[i]);
				BaseStream.WriteByte(0);
			}
		}

		public void WriteCompound(string name, Action<NBTWriter> onCompound) {
			BaseStream.WriteByte(10);
			WriteValue(name);
			onCompound(this);
			BaseStream.WriteByte(0);
		}
		public void WriteEmptyList(string name) { WriteListHeader(name, 0, 0); }
	}
}
