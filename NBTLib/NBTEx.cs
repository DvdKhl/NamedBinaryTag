using NBTLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTLib {
	public static class NBTReaderEx {
		public static NBTNode TreeToStructure(this NBTReader reader) {
			var node = new NBTNode {
				Name = reader.Name,
				Type = reader.Type
			};

			node.Value = reader.Value;

			if(reader.Type == NBTType.Compound) {
				var nodes = new List<NBTNode>();
				while(reader.MoveNext() && reader.Type != NBTType.End) {
					nodes.Add(TreeToStructure(reader));
				}
				node.Value = nodes;

			} else if(reader.Type == NBTType.CompoundList) {
				var nodes = new List<NBTNode>[(int)reader.Value];
				for(int i = 0; i < nodes.Length; i++) {
					nodes[i] = new List<NBTNode>();
					while(reader.MoveNext() && reader.Type != NBTType.End) {
						nodes[i].Add(TreeToStructure(reader));
					}
				}
				node.Value = nodes;
			}

			return node;
		}

		public static void Write(this NBTWriter writer, NBTNode node) {
			switch(node.Type) {
				case NBTType.End: writer.Write(node.Name, (byte)0); break;
				case NBTType.Byte: writer.Write(node.Name, (byte)node.Value); break;
				case NBTType.Short: writer.Write(node.Name, (short)node.Value); break;
				case NBTType.Int: writer.Write(node.Name, (int)node.Value); break;
				case NBTType.Long: writer.Write(node.Name, (long)node.Value); break;
				case NBTType.Float: writer.Write(node.Name, (float)node.Value); break;
				case NBTType.Double: writer.Write(node.Name, (double)node.Value); break;
				case NBTType.Binary: writer.Write(node.Name, (byte[])node.Value); break;
				case NBTType.String: writer.Write(node.Name, (string)node.Value); break;
				case NBTType.IntArray: writer.Write(node.Name, (int[])node.Value); break;
				case NBTType.List: writer.Write(node.Name, (byte)0); break;
				case NBTType.ShortList: writer.Write(node.Name, (short[])node.Value); break;
				case NBTType.IntList: writer.Write(node.Name, (int[])node.Value); break;
				case NBTType.LongList: writer.Write(node.Name, (long[])node.Value); break;
				case NBTType.FloatList: writer.Write(node.Name, (float[])node.Value); break;
				case NBTType.DoubleList: writer.Write(node.Name, (double[])node.Value); break;
				//case NBTType.BinaryList: writer.Write(node.Name, (byte[][])node.Value); break;
				case NBTType.StringList: writer.Write(node.Name, (string[])node.Value); break;

				case NBTType.Compound:
					writer.WriteCompound(node.Name, w => {
						foreach(var subNode in (List<NBTNode>)node.Value) {
							Write(w, subNode);
						}
					});
					break;

				case NBTType.CompoundList:
					writer.Write(node.Name, (List<NBTNode>[])node.Value, (w, item) => {
						foreach(var subNode in item) {
							Write(writer, subNode);
						}
					});
					break;

				default: throw new InvalidOperationException();
			}
		}

	}

	public class NBTNode {
		public string Name;
		public NBTType Type;
		public object Value;

		public override string ToString() { return Name + " = (" + Type + ")" + Value; }
	}
}
