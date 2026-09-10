CREATE TABLE [WebAppointment].[SystemUser] (
    [IDUsername]   INT           IDENTITY (1, 1) NOT NULL,
    [Username]     VARCHAR (20)  NOT NULL,
    [Password]     VARCHAR (200) NOT NULL,
    [Email]        VARCHAR (150) NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [UpdateDate]   DATETIME      NULL,
    [Status]       BIT           NOT NULL,
    [NumMovil]     VARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED ([IDUsername] ASC)
);

