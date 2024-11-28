using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class bankAppScript : MonoBehaviour
{
    //objects created in unity
    //panel section
    public GameObject homeMenuPanel;
    public GameObject loginMenuPanel;
    public GameObject mainMenuPanel;
    public GameObject withdrawalMenuPanel;
    public GameObject transferMenuPanel;
    public GameObject checkBalanceMenuPanel;
    public GameObject transactionSuccessMenuPanel;

    //fields, texts and error messages
    public GameObject usernameField;
    public GameObject passwordField;
    public GameObject incorrectBankIDText;
    public GameObject bankIDField;
    public GameObject pinField;

    public GameObject withdrawAmountField;
    public GameObject transferAmountField;
    public GameObject withdrawErrorText;
    public GameObject actualBalanceText;
    public GameObject beneficiaryNumTextField;
    public GameObject transferError1;
    public GameObject transferError2;
    public GameObject usernameTextMainMenu;
    public GameObject accountLockedText;
    public GameObject startAppButton;

    public string username;
    public string password;
    public string userSecurityAnswer;

    public string nameSurname = "Phil Foden";
    public string correctUsername = "philWalter47";
    public string correctPassword = "trebleWinner23'";
    public string correctSecurityAns = "Cristiano Ronaldo";
    public int availableBalace = 1000;
    public int pin = 1234;

    private string baseURL = "https://jsonplaceholder.typicode.com/users/";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //displaying the main menu and disabling all the other menus
    public void displayMainMenu()
    {
        mainMenuPanel.SetActive(true);
        withdrawalMenuPanel.SetActive(false);
        transferMenuPanel.SetActive(false);
        checkBalanceMenuPanel.SetActive(false);
    }

    //method to check user login details
    public void loginButtonClicked()
    {
        string bankID = bankIDField.GetComponent<Text>().text;
        string pinLogin = pinField.GetComponent<Text>().text;

        StartCoroutine(AuthenticateUser(bankID));
    }
    //method to autheniticate user info entered
    IEnumerator AuthenticateUser(string bankID)
    {
        string url = baseURL + bankID;
        using (var www = new WWW(url))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log("Login successful");
                incorrectBankIDText.SetActive(false);
                //setting the main menu panel
                loginMenuPanel.SetActive(false);
                mainMenuPanel.SetActive(true);
            }
            else
            {
                Debug.Log("User not found");
                incorrectBankIDText.SetActive(true);
                // Display error message
            }
        }
    }

    //setting login menu active
    public void loginMenu()
    {
        loginMenuPanel.SetActive(true);
        homeMenuPanel.SetActive(false);
    }

    //on main menu user picks an option they want to use in the banking app between withdraw, transfer, edit profile, view account, check balance and log out
    public void withdrawMenuActive()
    {
        mainMenuPanel.SetActive(false);
        withdrawalMenuPanel.SetActive(true);
    }
    //displays transfer menu/panel and switches off main menu panel
    public void transferMenuActive()
    {
        mainMenuPanel.SetActive(false);
        transferMenuPanel.SetActive(true);
    }
    //displays check balance menu/panel and switches off main menu panel
    public void checkBalanceMenuActive()
    {
        mainMenuPanel.SetActive(false);
        checkBalanceMenuPanel.SetActive(true);
    }

    //method to allow the user to withdraw a certain amount of money
    public void withdrawButtton()
    {
        int userWithdrawalAmount;

        //checking if the user entered a valid input 
        if(int.TryParse(withdrawAmountField.GetComponent<Text>().text, out userWithdrawalAmount))
        {
            //if entered amount is valid it is subtracted from the available balance and updates it
            if(userWithdrawalAmount<=availableBalace)
            {
                availableBalace -= userWithdrawalAmount;
                withdrawalMenuPanel.SetActive(false);
                transactionSuccessMenuPanel.SetActive(true);
            }
            //otherwise an error is displayed
            else
            {
                withdrawErrorText.SetActive(true);
            }
        }
        //error displayed to tell the user that they entered an invalid input
        else
        {
            withdrawErrorText.SetActive(true);
        }
    }

    //shows the user what their balance at this time is
    public void displayAvailableBalance()
    {
        actualBalanceText.GetComponent<Text>().text = "R" + availableBalace.ToString() + ".00";
    }

    //NOTE: if you use the withdraw menu before the transfer menu it won't work, and if you use it more than once it won't work, otherwise it will work
    //method to transfer money to beneficiaries
    public void transferMoneyButton()
    {
        int beneNum;
        
        if(int.TryParse(beneficiaryNumTextField.GetComponent<Text>().text, out beneNum))
        {
            int transferAmount;
            //error to display if the user chooses a beneficiary number outside the amount of users on the website
            if(beneNum > 10 || beneNum < 1)
            {
                transferError1.SetActive(true);
            }
            //checking if the user entered a value input as a transfer amount
            else if (int.TryParse(transferAmountField.GetComponent<Text>().text, out transferAmount))
            {
                //checking if amount is valid
                if (transferAmount < availableBalace)
                {
                    availableBalace -= transferAmount;
                    transactionSuccessMenuPanel.SetActive(true);
                    transferError1.SetActive(false);
                    transferError2.SetActive(false);
                }
                //else displaying error
                else
                {
                    transferError2.SetActive(true);
                }
            }
            //telling user they didnt select a valid beneficiary
            else
            {
                transferError2.SetActive(true);
            }
        }
        //telling user they didnt enter a valid input in the beneficiary field
        else
        {
            transferError1.SetActive(true);
        }
    }
}
//classes to manage website data being read in
[Serializable]
//user class to store user data
public class user
{
    public string id;
    public string name;
    public string username;
    public string email;
    public address address;
    public string phone;
    public string website;
    public company company;
}
//class to store user address
[Serializable]
public class address
{
    public string street;
    public string suite;
    public string city;
    public string zipcode;
    public geo geo;
}
//class to store where the company is locatedS
[Serializable]
public class geo
{
    public string lat;
    public string lng;
}
//class to store company details
[Serializable]
public class company
{
    public string name;
    public string catchPhrase;
    public string bs;
}

//class to parse/manage JSON array
public static class jsonManager
{
    //deserialize JSON data into object of type T
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }
    //class used as a container to hold a single value of type T
    [Serializable]
    private class Wrapper<T>
    {
        public List<T> items;
    }
}
