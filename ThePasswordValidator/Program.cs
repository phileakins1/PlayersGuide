// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Validator

//Console.WriteLine("Hello, World!");

Console.Title = "Validate Password";

PasswordValidator _validate = new("Abmmutlrem10");

Console.WriteLine($"\nThe {_validate.Password}'s password validity is {_validate.ValidatePassword()}.");

Console.ReadKey();



public class PasswordValidator
{
    public string Password { get; private set; }

    public PasswordValidator(string password)
    {
        Password = password;
    }

    public bool ValidatePassword()
    {
        int upperCase = 0, lowerCase = 0, numeric = 0;

        string password = Password;

        if (password.Length is < 6 or > 13)
        {
            Console.WriteLine("The password length constraints have not been met.");
            return false;
        }

        foreach (char c in password)
        {
            if (c == 'T' || c == '&')
            {
                Console.WriteLine($"An illegal character '{c}' has been used: complain to Ingmar in IT!\n");
                return false;
            }
            
            if (char.IsUpper(c))
            {
                upperCase++;
            }
            else if (char.IsLower(c))
            {
                lowerCase++;
            }
            else if (char.IsNumber(c))
            {
                numeric++;
            }
        }

        if (upperCase > 0 && lowerCase > 0 && numeric > 0)
        {
            return true;
        }
        else
            return false;
    }
}
