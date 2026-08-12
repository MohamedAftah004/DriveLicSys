# 🚗 Drivers and Vehicles License Department (DVLD)

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0078D7?style=for-the-badge&logo=windows&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

**DVLD** is a comprehensive desktop application designed to manage and streamline the operations of a Drivers and Vehicles License Department. Built with a robust **3-Tier Architecture**, it ensures a clean separation of concerns, scalability, and maintainability.

---

## 🏗️ Architecture
The project strictly follows a **3-Tier Architecture**:
1. **Presentation Layer (UI):** Built using Windows Forms (WinForms) for an intuitive user experience (`DVLD`).
2. **Business Logic Layer (BLL):** Handles the core rules, validations, and operations (`DVLD_Buisness`).
3. **Data Access Layer (DAL):** Manages all database interactions securely and efficiently (`DVLD_DataAccess`).

---

## ✨ Key Features

### 🔐 1. Secure Authentication & Login
- **Cryptography:** Passwords are encrypted and verified using `SHA-128` hashing.
- **Security Lockout:** System automatically locks out the user after 3 failed login attempts.
- **Audit Logging:** Login attempts, along with username and operation details, are securely recorded in the **Windows EventLog**.

### 👥 2. User & People Management
- Full **CRUD** operations (Create, Read, Update, Delete) for both Users and People.
- Advanced search, sorting, and filtering capabilities.
- Detailed views for displaying comprehensive user/person information.
- Main menu dashboard dynamically displays the currently logged-in user.

### 📄 3. License Applications Services
A complete suite of services for driving license management:
- **New Local Driving License:** Issue new licenses for local drivers.
- **New International License:** Issue international driving permits.
- **Renew License:** Process renewals for expired driving licenses.
- **Replacement (Lost/Damaged):** Issue replacements for lost or damaged licenses.
- **Release Detained License:** Handle the release process for detained licenses.

### 📝 4. Testing & Evaluation System
Comprehensive management of the driving test lifecycle:
- 👁️ **Vision Test**
- ✍️ **Written (Theory) Test**
- 🚗 **Practical (Street) Test**

---

## 🚀 Getting Started
1. Clone the repository.
2. Open the solution `.sln` file in Visual Studio.
3. Update the database connection string in the Data Access Layer to point to your local SQL Server instance.
4. Build and run the application.

---
*Developed as a complete solution for managing driving licenses and vehicle records.*
