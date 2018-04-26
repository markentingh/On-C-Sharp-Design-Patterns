# Project & Solution Structures
Below, I will discuss various types of folder & file structures used within many types of projects & solutions within Visual Studio. Hopefully, these naming conventions will bring some light on how to properly structure your projects.

### MVC Web Projects

* **Common**
  * Used for common classes used across your project, such as authentication & utility classes
* **Content**
  * A place to store protected content files that may be compiled by `gulp` and copied to the *wwwroot* folder. You may want to keep downloadable files here that only certain users have access to as well.
* **Controllers**
* **Components**
* **Models**
* **Resources**
  * Language-specific files and other international files
* **Scripts**
  * Used for storing javascript files that will later be compiled by `gulp` and copied to *wwwroot*
* **ViewModels**
  * A layer between models & views
* **Views**
  * **Shared**
    * Partial views, such as layout, header, and footer files
* **wwwroot**
  * All public-f acing files, such as images, js & css files, and downloadable content