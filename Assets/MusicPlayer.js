#pragma strict
 
 /*
  * Script written by ZimonD (http://answers.unity3d.com/users/31993/zimond.html)
  * Distributed under License GNU 1.3
  * This script is published without warranty.
  * If you modify this script, document it in this small description.
  * Modified by: *
 */
 
 public var controlledAudioSource: AudioSource;    // The audio source that will play the audio clips
 public var audioClips: AudioClip[];             // Array for the audio clips
 public var loop: boolean = true;                // Indicates whether the play list should loop when finished (start from beginning)
 private var timer: float;                         // Timer that keeps track of how long the audio clip has been playing
 private var currentAudioClipLength: float;         // Length of the active audio clip in seconds
 private var iterator: int = 0;                     // The index of the active audio clip
 private var playlistEnded: boolean = false;        // Whether or not the playlist has ended
 
 /*
  * Start function called at the beginning of the game.
 */
 function Start(): void {
 	reshuffle(audioClips);
     // If atleast one audio clip exists, start playing:
     if (audioClips.length > 0) {
         PlayCurrentClip();
     }
     
 }
 
 /*
  * Update function called each frame.
 */
 function Update(): void {
 
     // If atleast one audio clip exists and the play list has ended, update:
     if (audioClips.length > 0 && !playlistEnded) {
 
         // Increase timer with the time difference between this and the previous frame:
         timer += Time.deltaTime;
         
         // Check whether the timer has exceeded the length of the audio clip:
         if (timer > currentAudioClipLength) {
         
             // Either start from the beginning if the last clip is played
             // or go to next audio clip:
             if (iterator + 1 == audioClips.Length) {
             
                 if (loop) {
                 
                     // Set it to the first audio clip:
                     iterator = 0;
                     
                 } else {
                 
                     // Stop the active audio clip:
                     controlledAudioSource.Stop();
                     
                     // Set the playlist as ended:
                     playlistEnded = true;
                     
                     // No more playing, so return:
                     return;
                     
                 }
                 
             } else {
                 iterator++;
             }
             
             // Play the next audio clip:
             PlayCurrentClip();
             
         }
     
     }
     
 }
 
 /*
  * This function plays the current clip. It does not take
  * any parameters, as it accesses the global variables.
 */
 function PlayCurrentClip(): void {
 
         // Stop the active clip:
         controlledAudioSource.Stop();
 
         // Set the current clip as active audio clip:
         controlledAudioSource.clip = audioClips[iterator];
         
         // Set the length (in seconds) of the audio clip:
         currentAudioClipLength = audioClips[iterator].length;
         
         // Reset timer:
         timer = 0;
         
         // Play the clip:
         controlledAudioSource.Play();
         
 }
 
 /*
  * This function start the playlist from an specific index.
 */
 function PlayFromIndex( index: int ) {
 
     if (iterator + 1 <= audioClips.Length) {
 
         // Set the new start iterator:
         iterator = index;
         
         // Play the audio clip:
         PlayCurrentClip();
         
         // Start the playlist again:
         playlistEnded = false;
         
     } else {
     
         // This is not allowed:
         Debug.Log("Index " + index + " is out of the audio clip range.");
     
     }
     
 }
 
 function reshuffle(clips: AudioClip[]) {
 	for (var t = 0; t < clips.Length; t++ )
        {
            var clip : AudioClip = clips[t];
            var r : int = Random.Range(t, clips.Length);
            clips[t] = clips[r];
            clips[r] = clip;
        }
 }
 
 