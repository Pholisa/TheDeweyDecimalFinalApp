# TheDeweyDecimalFinalApp

# The Dewey Decimal Final App

## Introduction

This Windows Forms application has been developed using the .NET Framework 4.8, based on valuable feedback received from a lecturer. Initially, the application did not meet all the necessary requirements, particularly lacking the gamification feature. Additionally, the design was not impressive, and navigation between screens proved challenging.

In response to these issues, a new and improved version of the application has been created. The updated design now features easy navigation, with a side menu providing access to different screens.

## PART 1: Replacing Books

The "Replacing Books" section offers a gamified experience. It consists of two listviews; the bottom one is populated with random Dewey Decimal numbers. Users are required to drag and drop list items from the populated listview to the empty one, arranging them in ascending order. After this, users can click the check button to determine the correctness of their arrangement.

Gamification is incorporated through a progress bar, incrementing with each successful drag-and-drop action. The check button remains disabled until all items are moved to the top listview.

## PART 2: Identifying Areas

In the "Identifying Areas" section, the initial design and gamification scores have been revamped. Now, each time a user clicks a radio button, they receive a score to indicate their attempt at all the radio buttons.

## PART 3: Finding Call Numbers

This section introduces different levels of questions, visible through the provided buttons. Users need to select the correct answer by checking the checklist. If an incorrect choice is made, users can uncheck the selected answer, correct it, and then proceed to the next level. This functionality also has a reverse version.

The underlying tree structure is named DeweyNode, and the file containing all Dewey Decimal numbers information is located in bin > Debug, named DeweyDecData.

## GitHub Repository

For the latest updates and source code, visit the [GitHub repository](https://github.com/Pholisa/TheDeweyDecimalFinalApp.git).

