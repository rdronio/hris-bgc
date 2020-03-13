<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="HRIS_Basic.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- CSS -->
    <link rel="stylesheet" href="Styles/fontawesome/css/all.css" />
    <link rel="stylesheet" href="Styles/style.css" />
    <link
      rel="stylesheet"
      media="screen and (max-width: 767px)"
      href="Styles/mobile.css"
    />

    <!-- JavaScript -->
    <script src="Scripts/jquery-3.4.1.min.js"></script>

    <title>Time Keeping System</title>
  </head>

  <body id="error">
    <!-- Maintenance -->
    <section id="error">
      <header>
        <a href="https://systemoph.com/">
          <img src="Images/logo.png" alt="" />
        </a>
      </header>
      <main class="showcase container">
        <div>
          <img src="Images/error.png" alt="" />
        </div>
        <div class="lead">
          <h1>Sorry, the page not found.</h1>
          <p>
            We can't seem to find the page you're looking for.
          </p>
          <p class="error-code">Error Code: <span id="errorCode">404</span></p>
          <a href="Home.aspx" class="btn-md btn-primary " id="btnBack">
            Back to home
          </a>
        </div>
      </main>
      <footer>
        <h4>Follow Us!</h4>
        <a href="https://www.facebook.com/systemoph/"
          ><i class="fab fa-facebook-square"></i
        ></a>
        <a href=""><i class="fab fa-twitter-square"></i></a>
      </footer>
    </section>

    <script src="Scripts/main.js"></script>
  </body>
</html>