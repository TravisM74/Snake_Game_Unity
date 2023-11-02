using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public abstract class GameStateBase 
{
    /// <summary>
    /// REtuens if the sceneName is the name of  the currently loaded scene
    /// </summary>
    /// <param name="sceneName"> the name of the Scene</param>
    /// <returns>True if the loaded scene is sceneName false otherwise</returns>
    public static bool IsCurrentScene(String sceneName){
        return sceneName.ToLower() == SceneManager.GetActiveScene().name.ToLower();
    }
   public enum Type {
    None = 0,
    Menu,
    Options,
    GameOver,
    Level
   }

   List<Type> validTargetStates = new List<Type>();

   public abstract Type StateType {get;}
   public abstract string SceneName {get;}

/// <summary>
/// is this state active or not 
/// </summary>
/// <value>True, if the state is active false otherwise</value>
   public bool IsActive{
    get;
    private set;
   }

   public bool IsAdditive{
    get;
    private set;
   }

   protected GameStateBase (bool isAdditive = false){
    // TODO: Add functionality latter
    IsActive = false;
    IsAdditive = isAdditive;
   }

/// <summary>
/// Called when start Activated.
/// </summary>
   public virtual void Activate(){
    IsActive = IsCurrentScene(SceneName);
    if (!IsActive){
        // the correct scene is not loaded yet.
        LoadSceneMode loadMode = IsAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        SceneManager.LoadScene(SceneName, loadMode); // Loads the new scene
        IsActive = true;
    }
   }
  
  /// <summary>
  /// Called when state is deacitvated.
  /// </summary>
   public virtual void Deactivate(){
        IsActive = false;
        if (IsAdditive){
            SceneManager.UnloadSceneAsync(SceneName);
        }
   }

   protected bool AddTargetState(Type targetStateType){
    if (validTargetStates.Contains(targetStateType)){
        return false;
    }
    validTargetStates.Add(targetStateType);
    return true;
   }

   protected bool RemoveTargetState(Type targetStateType){
    return validTargetStates.Remove(targetStateType);
   }

   public bool IsValidTargetState(Type targetStateType){
    return validTargetStates.Contains(targetStateType);
   }
   
}
