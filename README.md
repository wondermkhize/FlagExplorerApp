# 🚩 Flag Explorer App

Hi there! 👋

Thanks for checking out the Flag Explorer App — a full-stack application featuring a .NET 8 Web API backend and a modern React frontend.

You can run both the backend and frontend using **Visual Studio Code (VS Code)** or your terminal. Follow the steps below to get everything running locally.

---

## 🔧 Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v16 or newer recommended)
- [npm](https://www.npmjs.com/) (comes with Node.js)
- [VS Code](https://code.visualstudio.com/) or any preferred terminal-based editor

---

## 📦 Clone the Repository

```bash
git clone https://github.com/wondermkhize/FlagExplorerApp.git
cd FlagExplorerApp
```

---

## 🛠 Backend (API) Setup

1. Open a terminal in VS Code and navigate to the API folder:

   ```bash
   cd API
   ```

2. Restore the .NET dependencies:

   ```bash
   dotnet restore
   ```

3. Run the backend:

   ```bash
   dotnet watch run
   ```

4. The backend will run on:

   - http://localhost:5000
   - https://localhost:5001

5. Access the Swagger documentation at:

   - http://localhost:5000/swagger/index.html
   - https://localhost:5001/swagger/index.html

🎉 **Your API is now running!** Use Swagger to explore the available endpoints.

---

## ✅ Backend Tests

To run backend unit tests:

1. From the root of the project or from the `BackEndTests` folder:

   ```bash
   dotnet test
   ```

This will compile and run all tests in the `BackEndTests` project and output results to the terminal.

---

## 🎨 Frontend (React) Setup

1. Open a new terminal window/tab and navigate to the frontend project:

   ```bash
   cd client
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. (Optional) Build the project:

   ```bash
   npm run build
   ```

4. Start the development server:

   ```bash
   npm run dev
   ```

Your frontend will typically be available at:

```
http://localhost:5173
```

---

## ✅ Frontend Tests

To run frontend tests:

```bash
npm run test
```

This uses the test runner configured in your frontend setup (e.g., Vitest, Jest, etc.). Make sure you have your test framework installed and configured.

---

## You're All Set! 🚀

- API available at `https://localhost:5001/swagger/index.html`
- Frontend available at `http://localhost:5173`
- Run tests with `dotnet test` and `npm run test`

Now go explore some flags! 🏳️‍🌈🏴‍☠️🎌
