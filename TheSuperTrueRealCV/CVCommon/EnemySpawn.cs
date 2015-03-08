using CVCommon.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CVCommon
{
    public class EnemySpawn : Entity
    {

        public EnemySpawn(Texture2D texture, Vector2 position, Vector2 size, string Name) : base(texture, position, size)
        {
            EnemyName = Name;
        }

        public EnemySpawn()
            : base(ContentHolder.tmp, Vector2.Zero, Settings.objectSize)
        { }

        public string EnemyName { get; set; }
    }
}
