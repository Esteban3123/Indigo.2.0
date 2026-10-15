CREATE TABLE [dbo].[HCTURNESP] (
    [CONSECUTI] INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI] CHAR (3)                                                                      NOT NULL,
    [FECHAINIC] DATETIME                                                                      NOT NULL,
    [FECHAFINA] DATETIME                                                                      NOT NULL,
    [CODCENATE] CHAR (10)                                                                     NOT NULL,
    [OBSERVACI] CHAR (500)                                                                    NULL,
    [CONFREPET] NVARCHAR (MAX)                                                                NULL,
    [CODUSUARI] CHAR (20)                                                                     CONSTRAINT [DF_HCTURNESP_CODUSUARI] DEFAULT ('Codigo de usuario') NOT NULL,
    [TODOELDIA] BIT                                                                           NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCTURNESP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCTURNESP_HCTURNESP] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCTURNESP] ([CONSECUTI]),
    CONSTRAINT [FK_HCTURNESP_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCTURNESP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCTURNESP] NOCHECK CONSTRAINT [FK_HCTURNESP_ADCENATEN];


GO
ALTER TABLE [dbo].[HCTURNESP] NOCHECK CONSTRAINT [FK_HCTURNESP_HCTURNESP];


GO
ALTER TABLE [dbo].[HCTURNESP] NOCHECK CONSTRAINT [FK_HCTURNESP_INESPECIA];


GO
ALTER TABLE [dbo].[HCTURNESP] NOCHECK CONSTRAINT [FK_HCTURNESP_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información de recurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNESP', @level2type = N'COLUMN', @level2name = N'CONFREPET';

