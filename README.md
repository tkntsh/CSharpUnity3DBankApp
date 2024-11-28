# Bank Application Script

The `bankAppScript` is a Unity C# script for creating a simple banking application prototype. It enables users to perform essential banking tasks, including logging in, checking their balance, withdrawing funds, and transferring money. The script also integrates with an external API for user authentication.

---

## Features

### 1. **User Authentication**
- Simulates login using a Bank ID and PIN.
- Authenticates user credentials against data retrieved from a JSONPlaceholder API.

### 2. **Banking Operations**
- **Withdraw Money**: Allows users to withdraw funds with validation for input and sufficient balance.
- **Transfer Money**: Enables fund transfers to beneficiaries, with error handling for invalid inputs.
- **Check Balance**: Displays the user's current balance.

### 3. **UI Panel Management**
- Toggles between various panels:
  - Home Menu
  - Login Menu
  - Main Menu
  - Withdrawal Menu
  - Transfer Menu
  - Check Balance Menu
  - Transaction Success Menu
- Displays appropriate error messages for invalid operations.

### 4. **Data Handling**
- Uses Unity’s `JsonUtility` to parse and manage JSON data.
- Stores and processes user, address, and company details from the API.

---

## Setup Instructions

1. **Unity Scene Setup**:
   - Add the `bankAppScript` to a GameObject in your scene.
   - Assign the following UI elements to the corresponding fields in the Inspector:
     - Panels (e.g., `homeMenuPanel`, `loginMenuPanel`)
     - Input Fields (e.g., `bankIDField`, `withdrawAmountField`)
     - Text Elements for error messages and balance display.

2. **API Integration**:
   - The script uses [JSONPlaceholder](https://jsonplaceholder.typicode.com/) as a mock API.
   - Ensure internet connectivity for fetching data during login.

3. **Customization**:
   - Update default credentials (`correctUsername`, `correctPassword`, `correctSecurityAns`) and balances (`availableBalance`) as needed.

---

## Key Classes and Methods

### **Main Methods**
- `loginButtonClicked()`: authenticates user credentials and toggles UI panels.
- `withdrawButtton()`: processes withdrawal requests with validation.
- `transferMoneyButton()`: handles fund transfers to beneficiaries.
- `displayAvailableBalance()`: updates and shows the user’s current balance.

### **Helper Classes**
- `user`, `address`, `geo`, `company`: serializable classes for storing API data.
- `jsonManager`: provides utility functions for parsing JSON data into objects.

---

## Skills Demonstrated
- user authentication and external API integration.
- UI management and panel navigation in Unity.
- input validation and error handling for user interactions.
- JSON parsing and data handling with Unity's `JsonUtility`.
- modular and reusable code architecture.

---

## Notes
- **Known Issues**: the transfer feature has specific limitations:
  - It may not work properly if the withdraw menu is used first or used repeatedly without refreshing.
- To extend functionality, consider adding encryption for sensitive data and session management.

---

## License
This script is open-source and available for educational and non-commercial use. Attribution is appreciated.
