using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class DarkWizard : MonoBehaviour
{
    Transform player;
    Animator anim;
    public AudioSource source;
    public AudioClip warningSound, breakSound;
    public float moveSpeed;
    public Transform startTargetPoint;

    bool achievedStartTargetPos = false;
    bool broken = false;
    Vector3 oldPos;

    [Range(100, 1000)]
    public int finalScreenWidth, finalScreenHeight;
    int initialScreenWidth, initialScreenHeight;

    const float approachDistSqr = 0.5f;

    //GuideMaster gm;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        anim.SetBool("moving", true);
        oldPos = transform.position;
        transform.LookAt(startTargetPoint);
    }

    void Start()
    {
        Cursor.visible = false;
        //gm = GuideMaster.instance;
    }
    
    void FixedUpdate()
    {
        if (broken) return;

        if (achievedStartTargetPos)
        {
            if (Vector3.SqrMagnitude(transform.position - player.position) <= 16) //dist <= 4
            {
                StartCoroutine(Break());
            }
        }
        else
        {
            transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;

            if (Vector3.SqrMagnitude(transform.position - startTargetPoint.position) <= approachDistSqr)
            {
                achievedStartTargetPos = true;
                anim.SetBool("moving", false);
                transform.LookAt(oldPos);
                PlayClip(warningSound);
            }
        }
    }

    void PlayClip(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    IEnumerator Break()
    {
        broken = true;
        PlayClip(breakSound);
        initialScreenWidth = Screen.width;
        initialScreenHeight = Screen.height;

        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<FirstPersonController>().enabled = false;

        yield return new WaitForSeconds(3);
        Screen.SetResolution((int)Mathf.Lerp(finalScreenWidth, initialScreenWidth, 2f / 3), (int)Mathf.Lerp(finalScreenHeight, initialScreenHeight, 2f / 3), false);
        yield return new WaitForSeconds(1.5f);
        Screen.SetResolution((int)Mathf.Lerp(finalScreenWidth, initialScreenWidth, 1f / 3), (int)Mathf.Lerp(finalScreenHeight, initialScreenHeight, 1f / 3), false);
        yield return new WaitForSeconds(1);
        Screen.SetResolution(finalScreenWidth, finalScreenHeight, false);
        yield return new WaitForSeconds(1);
        source.clip = warningSound;
        source.Play();
        yield return new WaitForSeconds(1.5f);

        var folderNames = new List<string>() { "The Evil Folder", "Some Bad Stuff", "DANGER!!!", "Nothing at all", "Joking!" };
        StartCoroutine(CreateFolders(0.2f, folderNames));
        source.clip = breakSound;
        source.Play();
        yield return new WaitForSeconds(3);

        List<string> textsToWrite = new List<string>() { "You are going to die!", "Your soul is mine!!!", "HAHAHAHAHAHA", "I'M FREE! I'M FINALLY FREE!!!" };

        StartCoroutine(WriteTexts(0.1f, new List<string>() { "Some Bad Stuff", "The Evil Folder", "DANGER!!!" }, textsToWrite));

        yield return new WaitForSeconds(6);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    string path = @"C:\Users\Nika\Desktop";

    IEnumerator CreateFolders(float waitTime, List<string> folderNames)
    {
        anim.SetTrigger("Attack");

        while (folderNames.Count > 0)
        {
            int index = Random.Range(0, folderNames.Count);

            yield return new WaitForSeconds(waitTime);
            Directory.CreateDirectory(path + "\\" + folderNames[index]);

            folderNames.RemoveAt(index);
        }
    }

    IEnumerator WriteTexts(float waitTime, List<string> folderNames, List<string> textsToWrite)
    {
        source.clip = breakSound;
        source.Play();
        anim.SetTrigger("Attack");

        while (textsToWrite.Count > 0)
        {
            string text = textsToWrite[Random.Range(0, textsToWrite.Count)];

            using (StreamWriter writer = new StreamWriter(new FileStream(path + "\\" + folderNames[Random.Range(0, folderNames.Count)] + "\\" + text + ".txt", FileMode.Create)))
            {
                writer.Write(text);
            }

            textsToWrite.Remove(text);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
