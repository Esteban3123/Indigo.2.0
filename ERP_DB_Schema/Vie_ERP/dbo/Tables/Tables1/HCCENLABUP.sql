CREATE TABLE [dbo].[HCCENLABUP] (
    [ID]          INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCCENLABU] INT                                                                           NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_HCCENLABUP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCENLABUP_HCCENLABU] FOREIGN KEY ([IDHCCENLABU]) REFERENCES [dbo].[HCCENLABU] ([ID]),
    CONSTRAINT [FK_HCCENLABUP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCCENLABUP] NOCHECK CONSTRAINT [FK_HCCENLABUP_HCCENLABU];


GO
ALTER TABLE [dbo].[HCCENLABUP] NOCHECK CONSTRAINT [FK_HCCENLABUP_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Profesional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla HCCENLABU ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUP', @level2type = N'COLUMN', @level2name = N'IDHCCENLABU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUP', @level2type = N'COLUMN', @level2name = N'ID';

