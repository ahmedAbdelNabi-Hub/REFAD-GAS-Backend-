# Learning Management System Backend (ASP.NET Core)

A scalable and secure backend for a **Learning Management System (LMS)**, built with **ASP.NET Core** and following the **Onion Architecture**.  
This backend provides features for **user management**, **course creation**, **enrollment**, **assignments**, **assessments**, and **payments**.  
It supports **role-based access control** (Super Admin, Sub Admin, Instructor, Online Student, Offline Student) and integrates with **Paymob** for payments.

---

## 🚀 Features

- **Authentication & Authorization**
  - JWT-based authentication
  - Role-based access (Super Admin, Sub Admin, Instructor, Student Online, Student Offline)

- **User & Role Management**
  - Create and manage users with different roles
  - Manage instructors, students, and admins

- **Course Management**
  - Create, update, assign, and delete courses
  - Support for online and offline learning modes

- **Assignments & Assessments**
  - Create and submit assignments
  - Auto/manual grading system
  - Instructor feedback system

- **Payments**
  - **Paymob integration** for course fees and subscription payments
  - Payment history and status tracking

- **Reports & Analytics**
  - Student progress tracking
  - Course completion rates
  - Instructor performance reports

- **Architecture**
  - Built using **Onion Architecture** for separation of concerns
  - Scalable and maintainable structure

---

## 🏗️ Tech Stack

- **Backend Framework:** ASP.NET Core 8
- **Architecture:** Onion Architecture
- **Database:** SQL Server (via Entity Framework Core)
- **Authentication:** JWT + ASP.NET Identity
- **API:** RESTful APIs (GraphQL optional)
- **Payment Gateway:** Paymob
- **Containerization:** Docker (optional)
- **Testing:** xUnit / NUnit

---

---

## ⚙️ Setup & Installation

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/) (optional, for containerized deployment)

### Clone the Repository
```bash
git clone https://github.com/your-username/learning-management-system-backend.git
cd learning-management-system-backend


## 📂 Project Structure (Onion Architecture)

