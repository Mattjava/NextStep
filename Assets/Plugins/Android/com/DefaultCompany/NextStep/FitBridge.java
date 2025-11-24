package com.DefaultCompany.NextStep;

import android.app.Activity;
import android.content.Intent;
import android.util.Log;

import com.google.android.gms.auth.api.signin.GoogleSignIn;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.fitness.Fitness;
import com.google.android.gms.fitness.FitnessOptions;
import com.google.android.gms.fitness.data.DataType;
import com.google.android.gms.fitness.data.Field;
import com.google.android.gms.fitness.request.DataReadRequest;
import com.google.android.gms.fitness.result.DataReadResponse;
import com.google.android.gms.tasks.Task;

import java.util.Calendar;
import java.util.concurrent.TimeUnit;

// 👇 THIS IMPORT IS WHAT YOUR BUILD WAS MISSING
import com.unity3d.player.UnityPlayer;

public class FitBridge {
    private static final String TAG = "FitBridge";
    private final Activity activity;
    private static final int REQUEST_OAUTH_REQUEST_CODE = 1001;

    public FitBridge(Activity activity) {
        this.activity = activity;
    }

    public void requestPermissions() {
        FitnessOptions fitnessOptions = FitnessOptions.builder()
                .addDataType(DataType.TYPE_STEP_COUNT_DELTA, FitnessOptions.ACCESS_READ)
                .build();

        GoogleSignInAccount account = GoogleSignIn.getAccountForExtension(activity, fitnessOptions);

        if (!GoogleSignIn.hasPermissions(account, fitnessOptions)) {
            GoogleSignIn.requestPermissions(
                    activity,
                    REQUEST_OAUTH_REQUEST_CODE,
                    account,
                    fitnessOptions);
        } else {
            Log.i(TAG, "Already have Google Fit permissions.");
        }
    }

    public void readTodaySteps() {
        Calendar cal = Calendar.getInstance();
        long endTime = cal.getTimeInMillis();
        cal.set(Calendar.HOUR_OF_DAY, 0);
        cal.set(Calendar.MINUTE, 0);
        cal.set(Calendar.SECOND, 0);
        long startTime = cal.getTimeInMillis();

        FitnessOptions fitnessOptions = FitnessOptions.builder()
                .addDataType(DataType.TYPE_STEP_COUNT_DELTA, FitnessOptions.ACCESS_READ)
                .build();

        GoogleSignInAccount account = GoogleSignIn.getAccountForExtension(activity, fitnessOptions);
        if (account == null) {
            Log.e(TAG, "Google Fit account is null.");
            return;
        }

        DataReadRequest readRequest = new DataReadRequest.Builder()
                .aggregate(DataType.TYPE_STEP_COUNT_DELTA)
                .bucketByTime(1, TimeUnit.DAYS)
                .setTimeRange(startTime, endTime, TimeUnit.MILLISECONDS)
                .build();

        Task<DataReadResponse> response = Fitness.getHistoryClient(activity, account)
                .readData(readRequest);

        response.addOnSuccessListener(dataReadResponse -> {
            int totalSteps = 0;
            if (!dataReadResponse.getBuckets().isEmpty()) {
                totalSteps = dataReadResponse.getBuckets().get(0).getDataSets().get(0)
                        .getDataPoints().get(0).getValue(Field.FIELD_STEPS).asInt();
            }

            final int stepsToSend = totalSteps;
            Log.i(TAG, "Today's steps: " + stepsToSend);

            // 👇 Send back to Unity
            UnityPlayer.UnitySendMessage("FitBridgeWrapper", "OnStepsReceived", String.valueOf(stepsToSend));

        }).addOnFailureListener(e ->
                Log.e(TAG, "Error reading steps: " + e.getMessage()));
    }
}
