CREATE TABLE [WebAppointment].[HostessUser] (
    [IDHostessUser]        INT          IDENTITY (1, 1) NOT NULL,
    [IDSystemUser]         INT          NOT NULL,
    [DocumentType]         VARCHAR (2)  NOT NULL,
    [IdentificationNumber] VARCHAR (17) NOT NULL,
    [Hostename]            VARCHAR (50) NOT NULL,
    [Cellphone]            VARCHAR (10) NOT NULL,
    [Relationship]         VARCHAR (2)  NOT NULL,
    CONSTRAINT [PK_HostessUser] PRIMARY KEY CLUSTERED ([IDHostessUser] ASC),
    CONSTRAINT [FK__HostessUs__IDSys__0C1BC9F9] FOREIGN KEY ([IDSystemUser]) REFERENCES [WebAppointment].[SystemUser] ([IDUsername])
);

