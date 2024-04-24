### Air Force Management System

#### Table of Contents

1. [Short Description of Project](#short-description-of-project)
2. [Users of Application](#users-of-application)
3. [Functional Requirements](#functional-requirements)
4. [OOP Concepts](#oop-concepts)
5. [Design Pattern Implementation](#design-pattern-implementation)
6. [Classes](#classes)
7. [Future Directions](#future-directions)

---

#### Short Description of Project

The Air Force Management System is a centralized software solution designed to streamline administrative and operational tasks within an air force organization. It includes features for personnel management, mission tracking, flying hours recording, request management, and user-friendly console-based interface. The system enhances efficiency, decision-making, communication, and compliance within the air force, ultimately contributing to mission success.

---

#### Users of Application

This application caters to three main types of users:

1. **OC (Commanding Officer):** Responsible for assigning missions, managing under officers, checking missions, posting orders, and handling requests of officers.
   
2. **Officers:** Officers working under OC in specific trades. They can view assigned missions, update flying hours, complete missions, edit missions, request leaves or solve problems, and view their request statuses.

3. **IT Professionals:** Responsible for managing officers' accounts, adding or removing officers, assigning passwords, and handling officer applications.

---

#### Functional Requirements

| User Story ID | As a            | I want to perform            | So that I can             |
|---------------|-----------------|------------------------------|----------------------------|
| 1             | OC              | Assign Mission               | Assign missions to under officers   |
|               |                 | Check Mission                | Ensure completion of missions        |
|               |                 | Posting Order                | Post under officers to specific locations  |
|               |                 | Add Under Officer           | Add officers to under officers list  |
|               |                 | Check and Approve Requests  | Handle officer requests and approvals |
| 2             | Officers        | CRUD for Flying Hours (for GDP pilots only) | Track and update flying hours |
|               |                 | View Mission                 | View assigned missions       |
|               |                 | Complete Mission             | Mark missions as completed   |
|               |                 | Edit Mission                 | Modify mission details       |
|               |                 | Request                      | Request leaves or solve problems |
|               |                 | View Request                 | View request statuses        |
|               |                 | Delete Request               | Delete unnecessary requests  |
| 3             | IT              | Remove Officer               | Remove retired officers from the list |
|               |                 | Add Officer                  | Add new cadet credentials   |
|               |                 | Edit Account                 | Modify officer account details |
|               |                 | Assign Passwords             | Assign login credentials    |
|               |                 | View Officers                | View officers and search by PakNo |
|               |                 | View Application             | Review officer applications |

---

#### OOP Concepts

1. **Inheritance:** Utilized in classes like `AFPersonalle`, `Commanding Officers`, `InField Personalle`, and `GDPilot` to manage personnel details.

2. **Polymorphism:** Implemented as static polymorphism in `Validations` class for validation and dynamic polymorphism in `InField Personalle` class.

3. **Abstraction:** Interfaces applied in the DataLayer and abstraction applied to `InField Personalle` class for future flexibility.

4. **Association:** Aggregation among Commanding Officers and their Under Officers, and among Mission, Request, and GDPilots.

---

#### Design Pattern Implementation

- **Library Formation:** DLL created for BL and DL, used in both Winform and Console projects.
- **Object Handler:** Used to set interface objects as per requirements (e.g., database or file handling).
- **Singleton Principle:** Implemented in DL and IT Professionals for ensuring single instance access.

---

#### Classes

1. **AFPersonalle:** Contains basic credentials of personnel.
2. **Commanding Officers:** Manages commanding officers' details and their under officers.
3. **InField Personalle:** Abstract class managing personnel details in the field, inherited by GDPilots.
4. **GDPilot:** Represents pilots in the field.
5. **Missions:** Tracks mission details.
6. **Requests:** Manages officer requests.

---

#### Future Directions

1. Expansion to include Airman, JCO, and Civilians functionality.
2. Inclusion of other branches and integration of information about MARTYRS for digitalized support to their families.
3. Enhancement of security measures and digitalization of salary details and ranking upgradation system.

---

*Note: This is the first version proposal of the application and may require further enhancements and refinements in subsequent versions.*
