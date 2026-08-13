# Flag Explorer App

Flag Explorer App, a full stack project built with a .NET 8 Web API backend and a React frontend.

You can spin up both the backend and frontend using Visual Studio Code or your favorite terminal. Here is a quick guide to getting everything up and running on your local machine.

---

## Prerequisites

Before getting started make sure you have installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Node.js](https://nodejs.org/) (v16 or newer recommended)
* [npm](https://www.npmjs.com/) (included with Node.js)
* [VS Code](https://code.visualstudio.com/) or another text editor

---

## Getting the Code

Clone the repository to your machine and move into the project directory:

```bash
git clone https://github.com/wondermkhize/FlagExplorerApp.git
cd FlagExplorerApp

```

---

## Setting Up the Backend

1. Open your terminal and navigate to the API folder:
```bash
cd API

```


2. Restore the required .NET packages:
```bash
dotnet restore

```


3. Start the backend server:
```bash
dotnet watch run

```



Once running the backend listens at:

* http://localhost:5000
* https://localhost:5001

You can test out endpoints using the Swagger UI at:

* http://localhost:5000/swagger/index.html
* https://localhost:5001/swagger/index.html

---

## Running Backend Tests

To execute the test suite:

1. Navigate to the `BackEndTests` folder or stay in the project root:
```bash
dotnet test

```



This compiles the code and runs all unit tests displaying the results directly in your terminal.

---

## Setting Up the Frontend

1. Open a new terminal tab and head over to the client folder:
```bash
cd client

```


2. Install the Node packages:
```bash
npm install

```


3. Build the project (optional):
```bash
npm run build

```


4. Launch the local development server:
```bash
npm run dev

```



The React app should now be running at:

```
http://localhost:5173

```

---

## Running Frontend Tests

To run the frontend test suite execute:

```bash
npm run test

```

This triggers the configured test runner such as Vitest or Jest.

---

## Quick Summary

* Swagger API documentation: `https://localhost:5001/swagger/index.html`
* Web application interface: `http://localhost:5173`
* Test suites: `dotnet test` and `npm run test`
