using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Collectable : MonoBehaviour, ICoordinate
{
    public int score = 1;
    private new Renderer renderer;
    private Canvas canvas;

    public AnimationClip animationClip;
    public ParticleSystem collectEffectPrefab;

    public int X { get { return (int)transform.position.x; } }
    public int Y { get { return (int)transform.position.y; } }
  
  public virtual bool ShouldCollectCreateNew{
    get { return true;}
  }

    public void Setup() {
        renderer = gameObject.GetComponent<Renderer>();
        if (renderer == null) {
            Debug.LogError("A renderer is missing from the collectable!");
        }
        // find the canvas from a child module that is currently inactive
        canvas = gameObject.GetComponentInChildren<Canvas>(includeInactive:true);
        if (canvas == null) {
            Debug.LogError("Canvas not found on Collectable!");
        }
        TMP_Text  scoreText = GetComponentInChildren<TMP_Text>(includeInactive:true);
        if (scoreText != null) {
            scoreText.text = score.ToString();
        }

    }
    
    public virtual void OnCollect(SnakeController snake) {
        // 1. give player score 
        GameManager.Instance.AddScore(score);

        // 2. Grow the snake
        snake.Grow();

        // 3. notifiy gameboard the apple has been collected.
        
            LevelController.Current.GameBoard.NotifyItemCollected(this);


        // 4. Hide the apple
        renderer.enabled = false;
        float waitTime = animationClip.length;

        // 5. Enabale in world UI for the collectable
        canvas.gameObject.SetActive(true);

        // 6. play collect sound
        //AudioSource audio = GetComponent<AudioSource>();
        //if (audio != null) {
        //    audio.Play();
        //}
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null) {
            GameManager.Instance.AudioManager.PlaySoundEffect(audio, SoundEffectType.Collect);
        }

        // 6.5 partical effects played
        if (collectEffectPrefab != null) {
            ParticleSystem collectEffect = Instantiate(collectEffectPrefab, transform);
            collectEffect.Play(withChildren: true);
            if (collectEffect.main.duration >  waitTime){
                waitTime = collectEffect.main.duration;
            }
        }

        // 7. destroy the collectable
        StartCoroutine(DestoryCollectible(waitTime)); 

       
    }

    private IEnumerator DestoryCollectible(float waitTime) {
        
        yield return new WaitForSeconds(waitTime);  // waits for animation clip duration
        Destroy(gameObject);
    }
}
