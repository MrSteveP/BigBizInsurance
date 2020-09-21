namespace OnlineSchool.Application
{
    public class OnlineSchoolNavConstants
    {
        public static class ManagementAreas
        {
            public const string Area = "Management";
            public class Pages
            {
                public const string Index = "/Index";

                public const string ClassRoomsIndex = "/ClassRooms/Index";
                public const string ClassRoomsCreate = "/ClassRooms/Create";
                public const string ClassRoomsEdite = "/ClassRooms/Edite";
                public const string ClassRoomsDisplay = "/ClassRooms/Display";

                public const string MaterialsIndex = "/Materials/Index";
                public const string MaterialsCreate = "/Materials/Create";
                public const string MaterialsEdite = "/Materials/Edite";
                public const string MaterialsDisplay = "/Materials/Display";

                public const string SchoolsIndex = "/Schools/Index";
                public const string SchoolsCreate = "/Schools/Create";
                public const string SchoolsEdite = "/Schools/Edite";
                public const string SchoolsDisplay = "/Schools/Display";

                public const string SchoolAdminsIndex = "/SchoolAdmins/Index";
                public const string SchoolAdminsCreate = "/SchoolAdmins/Create";
                public const string SchoolAdminsEdite = "/SchoolAdmins/Edite";
                public const string SchoolAdminsDisplay = "/SchoolAdmins/Display";

                public const string GradesIndex = "/Grades/Index";
                public const string GradesCreate = "/Grades/Create";
                public const string GradesEdite = "/Grades/Edite";
                public const string GradesDisplay = "/Grades/Display";

                public const string StudentsIndex = "/Students/Index";
                public const string StudentsCreate = "/Students/Create";
                public const string StudentsEdite = "/Students/Edite";
                public const string StudentsDisplay = "/Students/Display";

                public const string ParentsIndex = "/Parents/Index";
                public const string ParentsCreate = "/Parents/Create";
                public const string ParentsEdite = "/Parents/Edite";
                public const string ParentsDisplay = "/Parents/Display";


                public const string TeachersIndex = "/Teachers/Index";
                public const string TeachersCreate = "/Teachers/Create";
                public const string TeachersCompleteInfo = "/Teachers/CompleteInfo";
                public const string TeachersEdite = "/Teachers/Edite";
                public const string TeachersDisplay = "/Teachers/Display";

                public const string SchedulesIndex = "/Schedules/Index";
                public const string ScheduleSettings = "/Schedules/Settings";
                public const string SchedulesCreate = "/Schedules/Create";
                public const string SchedulesEdite = "/Schedules/Edite";
                public const string SchedulesDisplay = "/Schedules/Display";
                public const string SchedulesCompleteInfo = "/Schedules/CompleteInfo";
                


            }
        }

        public static class TeacherAreas
        {
            public const string Area = "Teacher";
            public class Pages
            {
                public const string Index = "/Index";

                public const string SchedulesIndex = "/Schedules/Index";
                public const string SchedulesDisplay = "/Schedules/Display";

                public const string SchoolClassesIndex = "/SchoolClasses/Index";
                public const string SchoolClassesCreate = "/SchoolClasses/Create";
                public const string SchoolClassesEdite = "/SchoolClasses/Edite";
                public const string SchoolClassesCreateVideos = "/SchoolClasses/CreateVideos";
                public const string SchoolClassesCreateFiles = "/SchoolClasses/CreateFiles";
                public const string SchoolClassesCreateHomeworkMultichoices = "/SchoolClasses/CreateHomeworkMultichoices";
                public const string SchoolClassesCreateHomeworkText = "/SchoolClasses/CreateHomeworkText";
                public const string SchoolClassesCreateHomeworkTrueFalse = "/SchoolClasses/CreateHomeworkTrueFalse";
                public const string SchoolClassesCreateHomeworkResearch = "/SchoolClasses/CreateHomeworkResearch";

                public const string StudentAnswersIndex = "/StudentAnswers/Index";
                public const string StudentMultiChoicesAnswers = "/StudentAnswers/MultiChoicesAnswers";
                public const string StudentTextAnswers = "/StudentAnswers/TextAnswers";
                public const string StudentTrueFalseAnswers = "/StudentAnswers/TrueFalseAnswers";
                public const string StudentResearchAnswers = "/StudentAnswers/ResearchAnswers";
                public const string StudentHomeworkComplete = "/StudentAnswers/HomeworkComplete";

                public const string StudentsIndex = "/Students/Index";
                public const string StudentResults = "/Students/Results";

                
            }
        }

        public static class StudentAreas
        {
            public const string Area = "Student";
            public class Pages
            {
                public const string StudentIndex = "/Index";
                public const string StudentHomeworkIndex = "/Homework/Index";
                public const string StudentHomeworkAttendingClass = "/Homework/AttendingClass";
                public const string StudentHomeworkMultiChoicesQuestions = "/Homework/HomeworkMultiChoicesQuestions";
                public const string StudentHomeworkTextQuestions = "/Homework/HomeworkTextQuestions";
                public const string StudentHomeworkTrueFalseQuestions = "/Homework/HomeworkTrueFalseQuestions";
                public const string StudentHomeworkResearchQuestions = "/Homework/HomworkResearchQuestions";
                public const string StudentHomeworkHomeworkResult = "/Homework/HomeworkResult";

                public const string StudentHomeworkResultsList = "/HomeworkResults/Index";
                public const string StudentHomeworkMultiChoicesResults = "/HomeworkResults/MultiChoicesResults";
                public const string StudentHomeworkTextResults = "/HomeworkResults/TextResults";
                public const string StudentHomeworkTrueFalseResults = "/HomeworkResults/TrueFalseResults";
                public const string StudentHomeworkResearchResults = "/HomeworkResults/ResearchResults";

            }
        }

        public static class ParentAreas
        {
            public const string Area = "Parent";
            public class Pages
            {
                public const string Index = "/Index";

                public const string StudentsIndex = "/Students/Index";
                public const string StudentResults = "/Students/Results";


            }
        }

    }
}
