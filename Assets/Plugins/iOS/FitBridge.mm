#import <HealthKit/HealthKit.h>
#import <Foundation/Foundation.h>

@interface FitBridge : NSObject
@property (nonatomic, strong) HKHealthStore *healthStore;
@end

@implementation FitBridge

- (instancetype)init {
    self = [super init];
    if (self) {
        _healthStore = [[HKHealthStore alloc] init];
    }
    return self;
}

- (void)requestAuthorization {
    if (![HKHealthStore isHealthDataAvailable]) return;

    NSSet *readTypes = [NSSet setWithObjects:
                        [HKObjectType quantityTypeForIdentifier:HKQuantityTypeIdentifierStepCount],
                        nil];

    [self.healthStore requestAuthorizationToShareTypes:nil
                                             readTypes:readTypes
                                            completion:^(BOOL success, NSError *error) {
        if (!success) {
            NSLog(@"HealthKit auth failed: %@", error);
        }
    }];
}

- (void)getTodaySteps {
    HKQuantityType *stepType = [HKQuantityType quantityTypeForIdentifier:HKQuantityTypeIdentifierStepCount];
    NSPredicate *today = [HKQuery predicateForSamplesWithStartDate:[[NSCalendar currentCalendar] startOfDayForDate:[NSDate date]]
                                                           endDate:[NSDate date]
                                                           options:HKQueryOptionStrictStartDate];

    HKStatisticsQuery *query = [[HKStatisticsQuery alloc] initWithQuantityType:stepType
                                                       quantitySamplePredicate:today
                                                                       options:HKStatisticsOptionCumulativeSum
                                                             completionHandler:^(HKStatisticsQuery *query, HKStatistics *result, NSError *error) {
        double total = [result.sumQuantity doubleValueForUnit:[HKUnit countUnit]];
        NSString *stepsStr = [NSString stringWithFormat:@"%0.0f", total];

        UnitySendMessage("FitBridgeWrapper", "OnStepsReceived", [stepsStr UTF8String]);
    }];

    [self.healthStore executeQuery:query];
}
@end

extern "C" {
    void _RequestHealthKitPermissions() {
        [[[FitBridge alloc] init] requestAuthorization];
    }

    void _GetTodaySteps() {
        [[[FitBridge alloc] init] getTodaySteps];
    }
}
