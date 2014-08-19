using Shade.Helios.State;
using System.Collections.Generic;

namespace Shade.Helios
{
   public class SceneManager : ISceneManager
   {
      private readonly List<IScene> scenes = new List<IScene>();
      private IScene activeScene = new NullScene();

      public SceneManager(Engine engine)
      {
         engine.Services.AddService(typeof(ISceneManager), this);
      }

      public void AddScene(IScene scene) { this.scenes.Add(scene); }
      public void RemoveScene(IScene scene) { this.scenes.Remove(scene); }

      public IReadOnlyCollection<IScene> Scenes { get { return scenes; } }
      public IScene ActiveScene { get { return this.activeScene; } set { this.activeScene = value; } }
   }

   public interface ISceneManager
   {
      void AddScene(IScene scene);
      void RemoveScene(IScene scene);

      IReadOnlyCollection<IScene> Scenes { get; }
      IScene ActiveScene { get; set; }
   }
}