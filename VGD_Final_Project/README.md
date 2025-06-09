**Title:** Videogame Design Final
**Description:** Modified Unity tutorial with more advanced movement and modified graphics/sprites.

**Controls:**
Left and Right arrow keys or A and D to move sideways. Space bar to jump. Press shift to dash.

**Script Changes:**
There were only two scripts I modified, which were the 'PlayerController.cs' and 'DashSlider.cs' scripts. Specifically, in the PlayerController script I added a variable to track the amount of jumps so that I could correctly track when double jump was available for use. I also added another way of movement in the form of a short burst of movement speed that goes on a cooldown after activation. To visualize this cooldown, the DashSlider updates my dash icon to fill up as the cooldown decreases.

**Challenges:**
Movement like the double jump and dashing/dash cooldown were incredibly easy to integrate into the game, whereas the UI and visual for the dash cooldown I struggled with a lot. First of all, the icon I used for the dash visual wasn't moving with the hud, which I found out was due to conflicting layering with the camera and icon. Secondly, I wanted to add a visual of the icon filling up so that you could tell when your dash was ready which just took a lot of time and google searches in general. It was just a lot of debugging and trial and error. **But it worked in the end and I'm proud of it.**
