CREATE TABLE [dbo].[HCRADFIRMAS] (
    [ID]              INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADESQUEMAS] INT                                                                           NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI]       CHAR (3)                                                                      NOT NULL,
    [FECHACREA]       DATETIME                                                                      NOT NULL,
    [USUCREA]         CHAR (20)                                                                     NOT NULL,
    CONSTRAINT [PK_HCRADFIRMAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADFIRMAS_HCRADESQUEMAS] FOREIGN KEY ([IDHCRADESQUEMAS]) REFERENCES [dbo].[HCRADESQUEMAS] ([ID]),
    CONSTRAINT [FK_HCRADFIRMAS_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCRADFIRMAS_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCRADFIRMAS] NOCHECK CONSTRAINT [FK_HCRADFIRMAS_HCRADESQUEMAS];


GO
ALTER TABLE [dbo].[HCRADFIRMAS] NOCHECK CONSTRAINT [FK_HCRADFIRMAS_INESPECIA];


GO
ALTER TABLE [dbo].[HCRADFIRMAS] NOCHECK CONSTRAINT [FK_HCRADFIRMAS_INPROFSAL];

