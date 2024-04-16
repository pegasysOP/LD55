using System;
using System.Collections;
using UnityEngine;

public class RabbitMinigame : Minigame
{
    public override event EventHandler<MedalType> OnMinigameOver;
    protected override void OnMinigameStepOver(object sender, MedalType score)
    {
        medals.Add(score);

        minigameStepInstance.OnMinigameStepOver -= OnMinigameStepOver;
        Destroy(minigameStepInstance.gameObject);

        StartCoroutine(ShowMedalThenDoStuff(score));
    }

    private IEnumerator ShowMedalThenDoStuff(MedalType score)
    {
        medalSprite.sprite = GetMedalSpriteFromScore(score);
        medalSprite.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        medalSprite.gameObject.SetActive(false);

        DoStuff(score);
    }

    private void DoStuff(MedalType score)
    {
        //If there are more steps then get the next step and start it 
        if (steps.Count > 0)
            StartMinigameStep();
        else
        {
            float sum = 0;
            int total = 0;
            //Do our calculation here with all the scores
            foreach (MedalType medal in medals)
            {
                sum += (int)medal;
            }
            sum /= medals.Count;
            total = Mathf.FloorToInt(sum);

            OnMinigameOver.Invoke(this, (MedalType)total);
        }
    }
}
