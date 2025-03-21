﻿namespace DEBUG.BL.Helpers.EmailTemplates;

public class VerifyEmailTemplate
{
    public static string VerifyEmail => """
        <!DOCTYPE html>
        <head>
            <title>Confirm Email</title>
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <link rel="preconnect" href="https://unpkg.com">
            <link rel="stylesheet" href="https://unpkg.com/bootstrap@5.2.2/dist/css/bootstrap.css">
            <link rel="preconnect" href="https://fonts.googleapis.com">
            <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="">
            <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300:400&amp;family=Poppins:wght@300;500&amp;display=swap" rel="stylesheet">
            <style>
                body {
                    background-color: #f9f9f9;
                    padding-right: 10px;
                    padding-left: 10px;
                }

                .content {
                    background-color: #ffffff;
                    border-color: #e5e5e5;
                    border-style: solid;
                    border-width: 0 1px 1px 1px;
                    max-width: 600px;
                    width: 100%;
                    height: 420px;
                    margin-top: 60.5px;
                    margin-bottom: 31px;
                    border-top: solid 3px #8e2de2;
                    border-top: solid 3px -webkit-linear-gradient(to right, #8e2de2, #4a00e0);
                    border-top: solid 3px -webkit-linear-gradient(to right, #8e2de2, #4a00e0);
                    text-align: center;
                    padding: 100px 0px 0px;
                }

                h1 {
                    padding-bottom: 5px;
                    color: #000;
                    font-family: Poppins, Helvetica, Arial, sans-serif;
                    font-size: 28px;
                    font-weight: 400;
                    font-style: normal;
                    letter-spacing: normal;
                    line-height: 36px;
                    text-transform: none;
                    text-align: center;
                }

                h2 {
                    margin-bottom: 30px;
                    color: #999;
                    font-family: Poppins, Helvetica, Arial, sans-serif;
                    font-size: 16px;
                    font-weight: 300;
                    font-style: normal;
                    letter-spacing: normal;
                    line-height: 24px;
                    text-transform: none;
                    text-align: center;
                }

                p {
                    font-size: 14px;
                    margin: 0px 21px;
                    color: #666;
                    font-family: 'Open Sans', Helvetica, Arial, sans-serif;
                    font-weight: 300;
                    font-style: normal;
                    letter-spacing: normal;
                    line-height: 22px;
                    margin-bottom: 40px;
                }

                .btn-primary {
                    background: #8e2de2;
                    background: -webkit-linear-gradient(to right, #8e2de2, #4a00e0);
                    background: linear-gradient(to right, #8e2de2, #4a00e0);
                    border: none;
                    font-family: Poppins, Helvetica, Arial, sans-serif;
                    font-weight: 200;
                    font-style: normal;
                    letter-spacing: 1px;
                    text-transform: uppercase;
                    text-decoration: none;
                }

                footer {
                    max-width: 600px;
                    width: 100%;
                    height: 420px;
                    padding-top: 50px;
                    text-align: center;
                }

                small {
                    color: #bbb;
                    font-family: 'Open Sans', Helvetica, Arial, sans-serif;
                    font-size: 12px;
                    font-weight: 400;
                    font-style: normal;
                    letter-spacing: normal;
                    line-height: 20px;
                    text-transform: none;
                    margin-bottom: 5px;
                    display: block;
                }

                small:last-child {
                    margin-top: 20px;
                }

                a {
                    color: #bbb;
                    text-decoration: underline;
                }
            </style>
        </head>
        <div class="d-flex align-items-center justify-content-center">
            <div class="content">
                <h1>Hello, __$name!</h1>
                <h2>Verify Your Email Account</h2>
                <p>Thanks for creating your account on our platform! Please click on confirm button to validate your account.</p><a href="__$link" class="btn btn-primary btn-lg" type="button">Confirm Email</a></div>
        </div>
        <div class="d-flex align-items-center justify-content-center">
            <footer><small>Powered by Julien.js | A lightweight Node.js scaffold</small><small><a href="#" target="_blank">View Web Version</a> | <a href="#" target="_blank">Email Preferences</a> | <a href="#" target="_blank">Privacy Policy</a></small><small>If you have any quetions please contact us <a href="mailto:support@example.com" target="_blank">support@example.com</a>.<br><a href="#" target="_blank">Unsubscribe</a> from our mailing lists.</small></footer>
        </div>
        """;
}