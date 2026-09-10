CREATE TABLE [Audit].[BasicAudit] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Tag]             INT           NOT NULL,
    [Entity]          VARCHAR (100) NOT NULL,
    [RegisterId]      VARCHAR (20)  NULL,
    [UserCode]        VARCHAR (20)  NULL,
    [UserName]        VARCHAR (200) NULL,
    [UserMachine]     VARCHAR (50)  NULL,
    [Parameters]      VARCHAR (100) NULL,
    [ReportName]      VARCHAR (50)  NULL,
    [TransactionDate] DATETIME      NOT NULL,
    [Operation]       TINYINT       NOT NULL,
    [Company]         VARCHAR (2)   CONSTRAINT [DF_BasicAudit_07_2014_01_Company] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_BasicAudit_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Audit_BasicAudit_Company_Entity_Operation_INC_RegisterId]
    ON [Audit].[BasicAudit]([Company] ASC, [Entity] ASC, [Operation] ASC)
    INCLUDE([RegisterId]);

