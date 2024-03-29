# Strong Passwords Generator

![Screenshot](Documents/screenshot%20001.PNG)

Strong Password Generator is a program that allows you to create strong and random passwords for your online accounts and other purposes. It has the following features:

* It uses the CryptGenRandom() API function, which is a Windows API function that generates cryptographically secure random numbers. This ensures that the passwords are truly random and unpredictable, and not based on any predictable pattern or algorithm.

* It has a graphical user interface, which makes it easy and intuitive to use. You can see the generated password on the screen, and adjust the settings according to your preferences.

* It uses the .NET technology, which is a software framework that supports multiple programming languages and platforms. This means that the program can run on any CPU that supports the .NET framework.

* It saves your settings automatically, so you don't need to set everything up every time you start the program.

* It generates US ASCII chars’ passwords, which are passwords that consist of characters from the US ASCII character set. This includes upper- and lower-case letters, digits, and special symbols. You can select which character types to include in your password, and how many.

* It generates random bytes in HEX format, which are strings that consist of hexadecimal digits, from 0 to 9 and A to F. Hexadecimal digits are often used to represent binary data, such as encryption keys or hashes. You can choose the length of the hexadecimal string, from 1 to 16 bytes.

* It copies the generated password to the clipboard automatically, so you can easily paste it into the desired location, such as a website or an application. You can also copy the password manually by clicking on the "Copy to Clipboard" button.

* It allows you to select the length of the passwords, from 1 to 30 characters for US ASCII chars passwords, and from 1 to 16 bytes for HEX strings. You can use the input box to adjust the length. The longer the password, the stronger and more secure it is.


## Usage

To use the Strong Password Generator program, you need to follow these steps:

1. Download and install the program.
2. Run the program and select the password type you want to generate: US ASCII chars or HEX. You can switch between the types by clicking on the tabs at the top of the window.
3. Adjust the settings for the password type you selected. For US ASCII chars passwords, you can select the character types and the number of them in total. For HEX strings, you can select the count of bytes.
4. Click on the "Generate now" button to generate a password according to your settings. You can also press the Enter key or the spacebar to generate a password. You can generate as many passwords as you want by clicking on the "Generate now" button again.
5. Copy the generated password to the clipboard by clicking on the "Copy to Clipboard" button. You can also copy the password manually by selecting it and pressing Ctrl+C.
6. Paste the password into the desired location, such as a website or an application, by pressing Ctrl+V, or by using the Paste option from the right-click menu.

You have successfully used the Strong Password Generator program to create strong and random passwords. You can now use these passwords to protect your online accounts and other sensitive data!


## Building

Strong Password Generator uses the Microsoft Visual Studio 2022 for its builds.

To build Strong Password Generator from source files with Microsoft Visual Studio, you can use either the graphical mode or the command-line mode. Here are the instructions for both methods:

### Graphical mode
1. Open Microsoft Visual Studio and select Open a project or solution from the start page or the File menu.
2. Browse to the folder where the .sln file is located and select it. This will load the project in Microsoft Visual Studio.
3. Select the configuration for the project by using the drop-down menus on the toolbar. For example, you can choose Debug or Release for the configuration.
4. Build the project by clicking on the Build menu and selecting Build Solution. You can also use the keyboard shortcut Ctrl+Shift+B.
5. Run the project by clicking on the Debug menu and selecting Start Debugging. You can also use the keyboard shortcut F5

### Command-line mode
1. Open a Developer Command Prompt for Microsoft Visual Studio. You can find it in the Start menu under Microsoft Visual Studio Tools.
2. Navigate to the folder where the .sln file is located by using the cd command.
3. Invoke the MSBuild tool to build the project. You can specify various options and flags for the tool. For example, the following command builds the project with the Release configuration:
```
msbuild PassStrongGen.sln /p:Configuration=Release
```
4. Run your executable by doubleclicking on it.


## Authors

This program was written and is maintained by SV Foster.


## License

This program is available under EULA, see [EULA text file](EULA.txt) for the complete text of the license. This program is free for personal, educational and/or non-profit usage.
