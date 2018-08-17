USE MASTER     
GO     
     
-- 1) Check for the Database Exists .If the database is exist then drop and create new DB     
IF EXISTS (SELECT [name] FROM sys.databases WHERE [name] = 'CFSISv1' )     
DROP DATABASE CFSISv1     
GO     
     
CREATE DATABASE CFSISv1     
GO     
     
USE CFSISv1     
GO    

IF EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'AcademicGroup' )     
DROP TABLE AcademicGroup     
GO    

CREATE TABLE [dbo].[AcademicGroup] (
   [AcademicGroupId] [int] NOT NULL
      IDENTITY (1,1),
   [AcademicGroupCode] [nvarchar](256) NOT NULL,
   [AcademicGroupDesc] [nvarchar](max) NULL

   ,CONSTRAINT [PK_AcademicGroup] PRIMARY KEY CLUSTERED ([AcademicGroupId])
)


GO
CREATE TABLE [dbo].[College] (
   [CollegeId] [int] NOT NULL
      IDENTITY (1,1),
   [CollegeCode] [nvarchar](256) NOT NULL,
   [CollegeName] [nvarchar](256) NULL,
   [CollegeAddress] [nvarchar](max) NULL

   ,CONSTRAINT [PK_College] PRIMARY KEY CLUSTERED ([CollegeId])
)


GO

CREATE TABLE [dbo].[Course] (
   [CourseId] [int] NOT NULL
      IDENTITY (1,1),
   [CourseCode] [nvarchar](256) NOT NULL,
   [CourseTitle] [nvarchar](256) NULL,
   [CourseDesc] [nvarchar](max) NULL

   ,CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED ([CourseId])
)


GO
CREATE TABLE [dbo].[DocumentsType] (
   [DocumentTypeId] [int] NOT NULL
      IDENTITY (1,1),
   [DocumentCode] [nvarchar](256) NOT NULL,
   [DocumentDesc] [nvarchar](max) NULL

   ,CONSTRAINT [PK_DocumentsType] PRIMARY KEY CLUSTERED ([DocumentTypeId])
)


GO
CREATE TABLE [dbo].[PaymentMethod] (
   [PaymentMenthodId] [int] NOT NULL
      IDENTITY (1,1),
   [PaymentCode] [nvarchar](256) NOT NULL,
   [PaymentDesc] [nvarchar](max) NULL

   ,CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED ([PaymentMenthodId])
)


GO

CREATE TABLE [dbo].[Semester] (
   [SemesterId] [int] NOT NULL
      IDENTITY (1,1),
   [SemesterCode] [nvarchar](256) NOT NULL,
   [SemesterDesc] [nvarchar](max) NULL,
   [SemesterStartMonth] [datetime2] NULL,
   [SemesterEndMonth] [datetime2] NULL

   ,CONSTRAINT [PK_Semester] PRIMARY KEY CLUSTERED ([SemesterId])
)


GO
CREATE TABLE [dbo].[StudentAcademic] (
   [StudentAcademicId] [int] NOT NULL
      IDENTITY (1,1),
   [StudentId] [int] NULL,
   [TenthMark] [int] NULL,
   [TenthSchoolName] [nvarchar](256) NULL,
   [TenthSchoolType] [nvarchar](256) NULL,
   [TwelthMark] [int] NULL,
   [TwelthSchoolName] [nvarchar](256) NULL,
   [TwelthSchoolType] [nvarchar](256) NULL,
   [AcademicGroupId] [int] NULL

   ,CONSTRAINT [PK_StudentAcademic] PRIMARY KEY CLUSTERED ([StudentAcademicId])
)


GO
CREATE TABLE [dbo].[StudentDocuments] (
   [StudentDocumentId] [int] NOT NULL
      IDENTITY (1,1),
   [StudentId] [int] NOT NULL,
   [DocumentTypeId] [int] NOT NULL,
   [DocumentDesc] [nvarchar](max) NULL,
   [DocumentBinary] [varbinary](max) NULL

   ,CONSTRAINT [PK_StudentDocumentId] PRIMARY KEY CLUSTERED ([StudentDocumentId])
)


GO
CREATE TABLE [dbo].[StudentEnrollment] (
   [StudentEnrollId] [int] NOT NULL
      IDENTITY (1,1),
   [StudentId] [int] NOT NULL,
   [CourseId] [int] NOT NULL,
   [CollegeId] [int] NOT NULL,
   [EnrollmentYear] [datetime2] NULL,
   [EnrollmentDesc] [nvarchar](max) NULL

   ,CONSTRAINT [PK_StudentEnrollment] PRIMARY KEY CLUSTERED ([StudentEnrollId])
)


GO
CREATE TABLE [dbo].[StudentFees] (
   [StudentFeesId] [int] NOT NULL
      IDENTITY (1,1),
   [StudentSemesterId] [int] NOT NULL,
   [SemesterFees] [money] NULL,
   [FeesDesc] [nvarchar](max) NULL,
   [PaymentMenthodId] [int] NOT NULL,
   [DateOfPayment] [datetime2] NULL,
   [ChequeNumber] [nvarchar](256) NULL,
   [BankDetails] [nvarchar](256) NULL

   ,CONSTRAINT [PK_StudentFees] PRIMARY KEY CLUSTERED ([StudentFeesId])
)


GO
CREATE TABLE [dbo].[StudentPlacement] (
   [StudentPlacementId] [int] NOT NULL
      IDENTITY (1,1),
   [StudentId] [int] NOT NULL,
   [Organisation] [nvarchar](256) NOT NULL,
   [PlacementStartDate] [nvarchar](256) NOT NULL,
   [PlacementDesc] [nvarchar](max) NULL

   ,CONSTRAINT [PK_StudentPlacement] PRIMARY KEY CLUSTERED ([StudentPlacementId])
)


GO
CREATE TABLE [dbo].[Students] (
   [StudentId] [int] NOT NULL
      IDENTITY (1,1),
   [UserId] [int] NULL,
   [FirstName] [nvarchar](80) NULL,
   [LastName] [nvarchar](80) NULL,
   [Gender] [nvarchar](10) NULL,
   [Email] [nvarchar](256) NULL,
   [DOB] [datetime2] NOT NULL,
   [PlaceOfBirth] [nvarchar](100) NULL,
   [Address] [nvarchar](max) NULL,
   [City] [nvarchar](255) NULL,
   [District] [nvarchar](255) NULL,
   [PinCode] [int] NULL,
   [ProfilePicBinary] [varbinary](max) NULL,
   [PhoneNumber] [nvarchar](50) NULL,
   [Active] [bit] NULL,
   [CreatedOnUtc] [datetime2] NULL,
   [Deleted] [bit] NULL

   ,CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([StudentId])
)


GO
CREATE TABLE [dbo].[StudentSemester] (
   [StudentSemesterId] [int] NOT NULL
      IDENTITY (1,1),
   [StudentEnrollId] [int] NOT NULL,
   [SemesterId] [int] NOT NULL,
   [SemesterGrade] [numeric](10,0) NULL

   ,CONSTRAINT [PK_StudentSemester] PRIMARY KEY CLUSTERED ([StudentSemesterId])
)


GO
CREATE TABLE [dbo].[Users] (
   [UserId] [int] NOT NULL
      IDENTITY (1,1),
   [UserGuid] [uniqueidentifier] NULL,
   [Email] [nvarchar](256) NULL,
   [EmailConfirmed] [bit] NOT NULL,
   [Password] [nvarchar](max) NULL,
   [UserName] [nvarchar](256) NULL,
   [LastLoginDateUtc] [datetime2] NULL,
   [Active] [bit] NULL,
   [CreatedOnUtc] [datetime2] NULL,
   [MimeType] [nvarchar](40) NULL

   ,CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId])
)


GO



ALTER TABLE [dbo].[StudentAcademic] WITH CHECK ADD CONSTRAINT [FK_StudentAcademic_AcademicGroup]
   FOREIGN KEY([AcademicGroupId]) REFERENCES [dbo].[AcademicGroup] ([AcademicGroupId])

GO
ALTER TABLE [dbo].[StudentAcademic] WITH CHECK ADD CONSTRAINT [FK_StudentAcademic_Students]
   FOREIGN KEY([StudentId]) REFERENCES [dbo].[Students] ([StudentId])

GO
ALTER TABLE [dbo].[StudentAcademic] WITH CHECK ADD CONSTRAINT [FK_StudentAcademic_Students1]
   FOREIGN KEY([StudentId]) REFERENCES [dbo].[Students] ([StudentId])

GO
ALTER TABLE [dbo].[StudentDocuments] WITH CHECK ADD CONSTRAINT [FK_StudentDocuments_DocumentsType]
   FOREIGN KEY([DocumentTypeId]) REFERENCES [dbo].[DocumentsType] ([DocumentTypeId])

GO
ALTER TABLE [dbo].[StudentDocuments] WITH CHECK ADD CONSTRAINT [FK_StudentDocuments_Students]
   FOREIGN KEY([StudentId]) REFERENCES [dbo].[Students] ([StudentId])

GO
ALTER TABLE [dbo].[StudentEnrollment] WITH CHECK ADD CONSTRAINT [FK_StudentEnrollment_College]
   FOREIGN KEY([CollegeId]) REFERENCES [dbo].[College] ([CollegeId])

GO
ALTER TABLE [dbo].[StudentEnrollment] WITH CHECK ADD CONSTRAINT [FK_StudentEnrollment_Course]
   FOREIGN KEY([CourseId]) REFERENCES [dbo].[Course] ([CourseId])

GO
ALTER TABLE [dbo].[StudentEnrollment] WITH CHECK ADD CONSTRAINT [FK_StudentEnrollment_Students]
   FOREIGN KEY([StudentId]) REFERENCES [dbo].[Students] ([StudentId])

GO
ALTER TABLE [dbo].[StudentFees] WITH CHECK ADD CONSTRAINT [FK_StudentFees_PaymentMethod]
   FOREIGN KEY([PaymentMenthodId]) REFERENCES [dbo].[PaymentMethod] ([PaymentMenthodId])

GO
ALTER TABLE [dbo].[StudentFees] WITH CHECK ADD CONSTRAINT [FK_StudentFees_StudentSemester]
   FOREIGN KEY([StudentSemesterId]) REFERENCES [dbo].[StudentSemester] ([StudentSemesterId])

GO
ALTER TABLE [dbo].[StudentPlacement] WITH CHECK ADD CONSTRAINT [FK_StudentPlacement_Students]
   FOREIGN KEY([StudentId]) REFERENCES [dbo].[Students] ([StudentId])

GO
ALTER TABLE [dbo].[Students] WITH CHECK ADD CONSTRAINT [FK_Students_Users]
   FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([UserId])

GO
ALTER TABLE [dbo].[StudentSemester] WITH CHECK ADD CONSTRAINT [FK_StudentSemester_Semester]
   FOREIGN KEY([SemesterId]) REFERENCES [dbo].[Semester] ([SemesterId])

GO
ALTER TABLE [dbo].[StudentSemester] WITH CHECK ADD CONSTRAINT [FK_StudentSemester_StudentEnrollment]
   FOREIGN KEY([StudentEnrollId]) REFERENCES [dbo].[StudentEnrollment] ([StudentEnrollId])

GO

