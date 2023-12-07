# 4.1_Receipt_Panel
The Recipe consists of blocks serving as headings and command objects. These commands specify the machine's movements and encompass the relevant parameters for each movement. We organize the commands on the Recipe screen in the desired order. Additionally, we enable the user to modify the parameters within predetermined limits during execution.

To achieve this, I created separate pages, UserControls, for each command. These UserControls mimic the structure of the washing machine's panel as requested. To populate the UserControls with data, I transferred the default values, minimum and maximum values, and information about which blocks are active for each command and parameter from the Excel table to an SQL database. This SQL table became the central repository for the data used in both the application and the PLC.

After designing the page layout for the UserControls, I implemented queries in the background to retrieve the necessary data from SQL. I then wrote the corresponding code to fill the textboxes with the default values for each parameter on the command pages. It is worth mentioning that I handled the multiplication by ten for certain parameters carefully to avoid comma usage. Additionally, I developed the necessary processes to display and adjust comma-separated values appropriately.

![Adsız](https://github.com/alicanarmttt/4.1_Receipt_Panel/assets/131194727/917b5cac-19bb-4cd4-8c14-f538fbdd9825)

To enable the user to modify the parameters, I implemented the LostFocus event on the textboxes. This event triggers a method that ensures the data is displayed in the correct format when the user leaves the textbox.


I created the left panel as a Flow Layout panel. Commands and blocks added are created and added as buttons. The buttons representing blocks serve as main headings, while the smaller buttons represent commands and act as subheadings. There are separate forms for adding both types of buttons. We can't add a command without selecting a block first. The commands to be added are also created based on which block they belong to, retrieved from the SQL table.

When a block or command button is in the selected state, its background color turns green. The newly added command or block is placed below the selected one. However, a block button with the title control cannot be added between two commands. You can only add a new block when a block button is selected, and it is placed at the end, skipping the command buttons in between.

