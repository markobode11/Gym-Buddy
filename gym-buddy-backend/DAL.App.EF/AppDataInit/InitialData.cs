using System;
using System.Collections.Generic;

namespace DAL.App.EF.AppDataInit
{
    public static class InitialData
    {
        public static string[] Roles { get; } = {"Admin", "Mentor"};

        public static readonly (string name, string password, string firstName, string lastName, string? role)[] Users =
        {
            ("admin@ttu.ee", "Foobar1.", "Admin", "Admin", "Admin"),
            ("mabode@ttu.ee", "Foobar1.", "Marko", "Bode", null),
            ("second@ttu.ee", "Foobar1.", "Foo", "Bar", null),
            ("thingy@ttu.ee", "Foobar1.", "What", "The hell", null),
            ("ott@ttu.ee", "Foobar1.", "Ott", "Kiivikas", "Mentor"),
            ("larry@ttu.ee", "Foobar1.", "Larry", "Wheels", "Mentor"),
            ("marko@ttu.ee", "Foobar1.", "Marco", "Bodekul", "Mentor"),
        };

        public static readonly (string name, string description, int difficultyId)[] Exercises =
        {
            ("Squat", "King of all leg exercises. Feet about shoulder width. Deep breath and brace your core before each decent.", 3),
            ("Bench press", "King of all push exercises. Arms width should be what is comfortable for you. Push your shoulders back, arch your back and use leg drive upon push.", 3),
            ("Deadlift", "King of all exercises. Dont ego lift this exercise since big injuries can happen. Initialize lift with legs not back and keep your back straight or slightly bent to decrease risk of injury.", 3),
            ("Dip", "Good triceps exercise. Try to not move to front nor keep yourself too straight sing shoulder impingement is bad :(", 2),
            ("EZ bar curl", "Simple curl but ez bar makes it target more your biceps and less your forearms", 1),
            ("Pull up", "Good bodyweight back exercise. Hands pronated. Do not use momentum while pulling yourself up. Dont be a crossfitter :)", 2),
            ("Chin up", "Good bodyweight back exercise. Hands supinated. Do not use momentum.", 2),
            ("Incline bench press", "Can be done with dumbbells or a bar. Arch your back and use leg drive and control the weight through the entire movement.", 2),
            ("Quad extension", "Do not lock out your knees since it can cause knee issues. Control the weight each decent.", 1),
        };

        public static readonly (string name, string description, string duration, int difficultyId)[] Workouts =
        {
            ("Push", "Workout that includes exercises for shoulders, chest and triceps. Several compound movements and several isolation exercises.", "70", 3),
            ("Pull", "Workout that includes exercises for back and biceps. Several compound movements and several isolation exercises.", "70", 3),
            ("Legs", "Workout that includes exercises for legs and abs. Several compound movements and several isolation exercises.", "70", 3),
            ("Upper body", "Workout that includes exercises for back, biceps, shoulders, triceps and chest. Several compound movements and several isolation exercises.", "75", 2),
            ("Lower body", "Workout that includes exercises for legs, abs and posterior chain. Several compound movements and several isolation exercises.", "75", 2),
        };

        public static readonly (string name, string description)[] Splits =
        {
            ("PPL", "Push pull legs is an advanced split. Upon completion you can choose to rest 1 day or do it second time again."),
            ("Upper lower split", "Intermediate split. Work your upper and lower body followed by a rest day."),
            ("Bro split", "Each workout you train 1 muscle mainly. Not the best."),
            ("Full body", "Easy split.  3 times a week you train your full body."),
        };

        public static readonly (string name, string description, string goal)[] FullPrograms =
        {
            ("Bodybuilding program", "Train directly for muscle hypertrophy. Get big but not strong", "Build muscle"),
            ("Powerlifter program", "Train directly powerlifting. Get stronger but not a lot bigger.", "Get stronger"),
            ("Powerbuilding program", "Train both bodybuilding and powerlifting. Very optimal since you get stronger and bigger.", "Get stronger and add muscle"),
            ("Weight loss program", "Train a lot of high rep long duration exercises and do a lot of cardio while strictly following a high protein diet.", "Lose fat"),
            ("Healthy program", "Do some full body splits and moderate cardio to stay healthy.", "Stay healthy"),
        };

        public static readonly (string medicalName, string everydayName)[] Muscles =
        {
            ("pectoralis major", "chest"),
            ("articulatio humeri", "shoulders"),
            ("triceps brachii", "triceps"),
            ("atissimus dorsi", "lats"),
            ("bicpes brachii", "biceps"),
            ("quadriceps femoris", "quad"),
            ("rectus abdominis", "abs"),
        };

        public static readonly List<string> Difficulties = new()
        {
            "Easy", "Intermediate", "Advanced", "Expert"
        };

        public static readonly (string fullName, string specialty, string description, string email, int appUserId)[] Mentors =
        {
            ("Ott Kiivikas", "Bodybuilding", " Eesti Kultuurkapitali kehakultuuri ja spordi sihtkapitali aastapreemia (mitmekülgne meister – maailma tippu kuuluv kulturist, kes tegutseb aktiivselt ka treeneri, koolitaja ja spordijuhina. Isikliku eeskujuga on ta olnud pikki aastaid üks Eesti silmapaistvamaid liikumisharrastuse ja tervislike eluviiside eestkõneleja)", "ott@ttu.ee", 5),
            ("Larry Wheels", "Strength training", "Born on 3 December, 1994, in The Bronx, New York City, under the fire sign of Sagittarius, Larry “Wheels” Williams is an American powerlifter, bodybuilder, social media personality and personal trainer. After growing up on the tough streets of The Bronx, he was determined to become bigger and stronger so that nobody would dare mess with him. As he was poor, Larry had no other option but to perform simple exercises in his room, on a daily basis. Upon getting a job, he finally joined a gym and almost realized his full potential, but realized that he didn’t want to focus on being bigger, but rather challenge himself and break his own lifting records, and thus became a professional powerlifter. By 2017, he was already the world record holder in the 242-pound division, taking part in competitions organized by the World Raw Powerlifting Federation (WRPF). Although already famous, he gained new fans in 2018, when he admitted to using steroids in the past – instead of judging him, people respected his honesty and candid approach.", "larry@ttu.ee", 6),
            ("Marco Bodekul", "Everything", "Best at everything", "marko@ttu.ee", 7),
        };
    }
}