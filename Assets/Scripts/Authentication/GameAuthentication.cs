using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    private async void Awake()
    {
        var authCore = await new AuthenticationCore().Initialize();
    }
}

public class AuthenticationCore
{
    // Lazy Singleton
    private static AuthenticationCore instance = null;
    public static AuthenticationCore GetInstance()
    {
        if (instance == null) {
            instance = new AuthenticationCore();
        }
        return instance;
    }
    
    public AuthenticationCore()
    {
        SetupEvents();
    }
    
    public async Task<AuthenticationCore> Initialize()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        return this;
    }

    public async Task<AuthenticationCore> Login()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return this;
    }
    
    private void SetupEvents() {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };
 
        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }
}