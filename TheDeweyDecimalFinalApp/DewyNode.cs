using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeweyDecimalFinalApp
{
    public class DewyNode
    {
        public string DewyNum { get; }
        public string CatName { get; }
        public List<DewyNode> Children { get; }

        public DewyNode(string num, string name)
        {
            DewyNum = num;
            CatName = name;
            Children = new List<DewyNode>();
        }

        public void AddChild(DewyNode childNode)
        {
            Children.Add(childNode);
        }

        public string GetTree(int level)
        {
            StringBuilder treeStr = new StringBuilder();
            treeStr.AppendLine($"{new string('-', level)} Code: {DewyNum}, Name: {CatName}");

            foreach (var child in Children)
            {
                treeStr.AppendLine(child.GetTree(level + 1));
            }

            return treeStr.ToString();
        }

        public void AddChildWithSubcategories(string subcategoryNum, string subcategoryName, List<string> descriptions)
        {
            DewyNode subcategoryNode = new DewyNode(subcategoryNum, subcategoryName);

            foreach (var description in descriptions)
            {
                DewyNode descriptionNode = new DewyNode(description, ""); // Description nodes have no name
                subcategoryNode.AddChild(descriptionNode);
            }

            Children.Add(subcategoryNode);
        }
    }
}
