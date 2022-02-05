USE HelperLand_Database
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](50) NOT NULL,
	[StateId] [int] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactUs](
	[ContactUsId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Subject] [nvarchar](500) NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[UploadFileName] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[FileName] [varchar](500) NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[ContactUsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavoriteAndBlocked](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TargetUserId] [int] NOT NULL,
	[IsFavorite] [bit] NOT NULL,
	[IsBlocked] [bit] NOT NULL,
 CONSTRAINT [PK_FavoriteAndBlocked] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceRequestId] [int] NOT NULL,
	[RatingFrom] [int] NOT NULL,
	[RatingTo] [int] NOT NULL,
	[Ratings] [decimal](2, 1) NOT NULL,
	[Comments] [nvarchar](2000) NULL,
	[RatingDate] [datetime] NOT NULL,
	[OnTimeArrival] [decimal](2, 1) NOT NULL,
	[Friendly] [decimal](2, 1) NOT NULL,
	[QualityOfService] [decimal](2, 1) NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceRequest](
	[ServiceRequestId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[ServiceStartDate] [datetime] NOT NULL,
	[ZipCode] [nvarchar](10) NOT NULL,
	[ServiceHourlyRate] [decimal](8, 2) NULL,
	[ServiceHours] [float] NOT NULL,
	[ExtraHours] [float] NULL,
	[SubTotal] [decimal](8, 2) NOT NULL,
	[Discount] [decimal](8, 2) NULL,
	[TotalCost] [decimal](8, 2) NOT NULL,
	[Comments] [nvarchar](500) NULL,
	[PaymentTransactionRefNo] [nvarchar](50) NULL,
	[PaymentDue] [bit] NOT NULL,
	[ServiceProviderId] [int] NULL,
	[SPAcceptedDate] [datetime] NULL,
	[HasPets] [bit] NOT NULL,
	[Status] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[RefundedAmount] [decimal](8, 2) NULL,
	[Distance] [decimal](18, 2) NOT NULL,
	[HasIssue] [bit] NULL,
	[PaymentDone] [bit] NULL,
	[RecordVersion] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ServiceRequest] PRIMARY KEY CLUSTERED 
(
	[ServiceRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceRequestAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceRequestId] [int] NULL,
	[AddressLine1] [nvarchar](200) NULL,
	[AddressLine2] [nvarchar](200) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_ServiceRequestAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceRequestExtra](
	[ServiceRequestExtraId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceRequestId] [int] NOT NULL,
	[ServiceExtraId] [int] NOT NULL,
 CONSTRAINT [PK_ServiceRequestExtra] PRIMARY KEY CLUSTERED 
(
	[ServiceRequestExtraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Test](
	[TestId] [int] NOT NULL,
	[TestName] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[Mobile] [nvarchar](20) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[Gender] [int] NULL,
	[DateOfBirth] [datetime] NULL,
	[UserProfilePicture] [nvarchar](200) NULL,
	[IsRegisteredUser] [bit] NOT NULL,
	[PaymentGatewayUserRef] [nvarchar](200) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[WorksWithPets] [bit] NOT NULL,
	[LanguageId] [int] NULL,
	[NationalityId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Status] [int] NULL,
	[BankTokenId] [nvarchar](100) NULL,
	[TaxNo] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAddress](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AddressLine1] [nvarchar](200) NOT NULL,
	[AddressLine2] [nvarchar](200) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](20) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Mobile] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_UserAddresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zipcode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ZipcodeValue] [nvarchar](50) NOT NULL,
	[CityId] [int] NOT NULL,
 CONSTRAINT [PK_Zipcode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Rating] ADD  CONSTRAINT [DF_Rating_OnTimeArrival]  DEFAULT ((0)) FOR [OnTimeArrival]
GO
ALTER TABLE [dbo].[Rating] ADD  CONSTRAINT [DF_Rating_Friendly]  DEFAULT ((0)) FOR [Friendly]
GO
ALTER TABLE [dbo].[Rating] ADD  CONSTRAINT [DF_Rating_QualityOfService]  DEFAULT ((0)) FOR [QualityOfService]
GO
ALTER TABLE [dbo].[ServiceRequest] ADD  CONSTRAINT [DF_ServiceRequest_PaymentDue]  DEFAULT ((0)) FOR [PaymentDue]
GO
ALTER TABLE [dbo].[ServiceRequest] ADD  CONSTRAINT [DF_ServiceRequest_IsPet]  DEFAULT ((0)) FOR [HasPets]
GO
ALTER TABLE [dbo].[ServiceRequest] ADD  CONSTRAINT [DF_ServiceRequest_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ServiceRequest] ADD  CONSTRAINT [DF_ServiceRequest_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[ServiceRequest] ADD  CONSTRAINT [DF_ServiceRequest_Distance]  DEFAULT ((0)) FOR [Distance]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsRegisteredUser]  DEFAULT ((0)) FOR [IsRegisteredUser]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_WorksWithPets]  DEFAULT ((0)) FOR [WorksWithPets]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsApproved]  DEFAULT ((0)) FOR [IsApproved]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[UserAddress] ADD  CONSTRAINT [DF_UserAddresses_IsDefault]  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[UserAddress] ADD  CONSTRAINT [DF_UserAddresses_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_State]
GO
ALTER TABLE [dbo].[FavoriteAndBlocked]  WITH CHECK ADD  CONSTRAINT [FK_FavoriteAndBlocked_FavoriteAndBlocked] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[FavoriteAndBlocked] CHECK CONSTRAINT [FK_FavoriteAndBlocked_FavoriteAndBlocked]
GO
ALTER TABLE [dbo].[FavoriteAndBlocked]  WITH CHECK ADD  CONSTRAINT [FK_FavoriteAndBlocked_User] FOREIGN KEY([TargetUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[FavoriteAndBlocked] CHECK CONSTRAINT [FK_FavoriteAndBlocked_User]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_ServiceRequest] FOREIGN KEY([ServiceRequestId])
REFERENCES [dbo].[ServiceRequest] ([ServiceRequestId])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_ServiceRequest]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_User] FOREIGN KEY([RatingFrom])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_User]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_User1] FOREIGN KEY([RatingTo])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_User1]
GO
ALTER TABLE [dbo].[ServiceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ServiceRequest_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_ServiceRequest_User]
GO
ALTER TABLE [dbo].[ServiceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ServiceRequest_User1] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_ServiceRequest_User1]
GO
ALTER TABLE [dbo].[ServiceRequestAddress]  WITH CHECK ADD  CONSTRAINT [FK_ServiceRequestAddress_ServiceRequest] FOREIGN KEY([ServiceRequestId])
REFERENCES [dbo].[ServiceRequest] ([ServiceRequestId])
GO
ALTER TABLE [dbo].[ServiceRequestAddress] CHECK CONSTRAINT [FK_ServiceRequestAddress_ServiceRequest]
GO
ALTER TABLE [dbo].[ServiceRequestExtra]  WITH CHECK ADD  CONSTRAINT [FK_ServiceRequestExtra_ServiceRequest] FOREIGN KEY([ServiceRequestId])
REFERENCES [dbo].[ServiceRequest] ([ServiceRequestId])
GO
ALTER TABLE [dbo].[ServiceRequestExtra] CHECK CONSTRAINT [FK_ServiceRequestExtra_ServiceRequest]
GO
ALTER TABLE [dbo].[UserAddress]  WITH CHECK ADD  CONSTRAINT [FK_UserAddresses_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserAddress] CHECK CONSTRAINT [FK_UserAddresses_User]
GO
ALTER TABLE [dbo].[Zipcode]  WITH CHECK ADD  CONSTRAINT [FK_Zipcode_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Zipcode] CHECK CONSTRAINT [FK_Zipcode_City]
GO