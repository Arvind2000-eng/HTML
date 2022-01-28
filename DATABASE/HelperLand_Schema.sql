

CREATE TABLE [State] 
(
	Id int IDENTITY(1,1) NOT NULL,
	StateName nvarchar(50) NOT NULL,
	CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED(Id)
)



CREATE TABLE City  
(
	Id int IDENTITY(1,1) NOT NULL,
	CityName nvarchar(50) NOT NULL,
	StateId int NOT NULL,
	CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED(Id),
	FOREIGN KEY (StateId) REFERENCES [State](Id)
)



CREATE TABLE ContactUs
(
	ContactUsId int IDENTITY(1,1) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	Email nvarchar(200) NOT NULL,
	[Subject] nvarchar(500) NULL,
	PhoneNumber nvarchar(20) NOT NULL,
	[Message] nvarchar(max) NOT NULL,
	UploadFileName nvarchar(100) NULL,
	CreatedOn datetime NULL,
	CreatedBy int NULL,
	[FileName] nvarchar(500) NULL,

	CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED(ContactUsId)
)


CREATE TABLE [User] 
(
	UserId int IDENTITY(1,1) NOT NULL,
	FirstName nvarchar(100) NOT NULL,
	LastName nvarchar(100) NOT NULL,
	Email nvarchar(100) NOT NULL,
	[Password] nvarchar(100) NULL,
	Mobile nvarchar(20) NOT NULL,
	UserTypeId int NOT NULL,
	Gender int NULL,
	DateOfBirth datetime NULL,
	UserProfilePicture nvarchar(200) NULL,
	IsRegisteredUser bit NOT NULL,
	PaymentGatewayUserRef nvarchar(200) NULL,
	ZipCode nvarchar(20) NULL,
	WorksWithPets bit NOT NULL,
	LanguageId int NULL,
	NationalityId int NULL,
	CreatedDate datetime NOT NULL,
	ModifiedDate datetime NOT NULL,
	ModifiedBy int NOT NULL,
	IsApproves bit NOT NULL,
	IsActive bit NOT NULL,
	Isdeleted bit NOT NULL,
	[Status] int NULL,
	BankTokenId nvarchar(100) NULL,
	TaxNo nvarchar(50) NULL,

	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED(UserId)
)


CREATE TABLE FavoriteAndBlocked
(
	Id int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	TargetUserId int NOT NULL,
	IsFavorite bit NOT NULL,
	IsBlocked bit NOT NULL,

	CONSTRAINT [PK_FavoriteAndBlocked] PRIMARY KEY CLUSTERED(Id),
	FOREIGN KEY(UserId) REFERENCES [User](UserId),
	FOREIGN KEY(TargetUserId) REFERENCES [User](UserId)
)


CREATE TABLE ServiceRequest
(
	ServiceRequestId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	ServiceId int NOT NULL,
	ServiceStartDate datetime NOT NULL,
	ZipCode nvarchar(10) NOT NULL,
	ServiceHourlyRate decimal(5) NULL,
	ServiceHours float NOT NULL,
	ExtraHours float NULL,
	SubTotal decimal(5) NOT NULL,
	Discount decimal(5) NULL,
	TotalCost decimal(5) NOT NULL,
	Comments nvarchar(500) NULL,
	PaymentTransactionRefNo nvarchar(50) NULL,
	PaymentDue bit NOT NULL DEFAULT ((0)),
	ServiceProviderId int NULL,
	SPAcceptedDate datetime NULL,
	HasPets bit NOT NULL DEFAULT ((0)),
	[Status] int NULL,
	CreatedDate datetime NOT NULL DEFAULT (getdate()),
	ModifiedDate datetime NOT NULL DEFAULT (getdate()),
	ModifiedBy int NULL,
	RefundedAmount decimal(5) NULL,
	Distance decimal(9) NOT NULL DEFAULT((0)),
	HasIssue bit NULL,
	PaymentDone bit NULL,
	RecordVersion uniqueidentifier NULL,

	CONSTRAINT [PK_ServiceRequest] PRIMARY KEY CLUSTERED(ServiceRequestId),
	FOREIGN KEY(UserId) REFERENCES [User](UserId),
	FOREIGN KEY(ServiceProviderId) REFERENCES [User](UserId)
)


CREATE TABLE Rating
(
	RatingId int IDENTITY(1,1) NOT NULL,
	ServiceRequestId int NOT NULL,
	RatingFrom int NOT NULL,
	RatingTo int NOT NULL,
	Ratings decimal(5) NOT NULL,
	Comments nvarchar(2000) NULL,
	RatingDate datetime NOT NULL,
	OnTimeArrival decimal(5) NOT NULL DEFAULT((0)),
	Friendly decimal(5) NOT NULL DEFAULT((0)),
	QualityOfService decimal(5) NOT NULL DEFAULT((0)),

	CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED(RatingId),
	FOREIGN KEY(ServiceRequestId) REFERENCES [ServiceRequest](ServiceRequestId),
	FOREIGN KEY(RatingFrom) REFERENCES [User](UserId),
	FOREIGN KEY(RatingTo) REFERENCES [User](UserId)
)


CREATE TABLE ServiceRequestAddress
(
	Id int IDENTITY(1,1) NOT NULL,
	ServiceRequestId int NULL,
	AddressLine1 nvarchar(200) NULL,
	AddressLine2 nvarchar(200) NULL,
	City nvarchar(50) NULL,
	[State] nvarchar(50) NULL,
	PostalCode nvarchar(20) NULL,
	Mobile nvarchar(20) NULL,
	Email nvarchar(100) NULL,

	CONSTRAINT [PK_ServiceRequestAddress] PRIMARY KEY CLUSTERED(Id),
	FOREIGN KEY(ServiceRequestId) REFERENCES [ServiceRequest](ServiceRequestId)
)


CREATE TABLE ServiceRequestExtra
(
	ServiceRequestExtraId int IDENTITY(1,1) NOT NULL,
	ServiceRequestId int NOT NULL,
	ServiceExtraId int NOT NULL,

	CONSTRAINT [PK_ServiceRequestExtra] PRIMARY KEY CLUSTERED(ServiceRequestExtraId),
	FOREIGN KEY(ServiceRequestId) REFERENCES [ServiceRequest](ServiceRequestId)
)


CREATE TABLE UserAddress
(
	AddressId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	AddressLine1 nvarchar(200) NOT NULL,
	AddressLine2 nvarchar(200) NULL,
	City nvarchar(50) NOT NULL,
	[State] nvarchar(50) NULL,
	PostalCode nvarchar(20) NOT NULL,
	IsDefault bit NOT NULL DEFAULT((0)),
	IsDeleted bit NOT NULL DEFAULT((0)),
	Mobile nvarchar(20) NULL,
	Email nvarchar(100) NULL,

	CONSTRAINT [PK_UserAddresses] PRIMARY KEY CLUSTERED(AddressId),
	FOREIGN KEY(UserId) REFERENCES [User](UserId)
)



CREATE TABLE Zipcode
(
	Id int IDENTITY(1,1) NOT NULL,
	ZipcodeValue nvarchar(50) NOT NULL,
	CityId int NOT NULL,

	CONSTRAINT [PK_Zipcode] PRIMARY KEY CLUSTERED(Id),
	FOREIGN KEY(CityId) REFERENCES [City](Id)
)


CREATE TABLE [Test]
(
	TestId int NOT NULL,
	TestName nvarchar(50) NULL
)