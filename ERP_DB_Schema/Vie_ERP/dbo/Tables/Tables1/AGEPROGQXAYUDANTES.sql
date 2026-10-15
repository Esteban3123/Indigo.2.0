CREATE TABLE [dbo].[AGEPROGQXAYUDANTES] (
    [ID]          INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGEPROGQX] INT                                                                           NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_AGEPROGQXAYUDANTES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGEPROGQXAYUDANTES_AGEPROGQX] FOREIGN KEY ([IDAGEPROGQX]) REFERENCES [dbo].[AGEPROGQX] ([CODAUTONU]),
    CONSTRAINT [FK_AGEPROGQXAYUDANTES_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGEPROGQXAYUDANTES] NOCHECK CONSTRAINT [FK_AGEPROGQXAYUDANTES_AGEPROGQX];


GO
ALTER TABLE [dbo].[AGEPROGQXAYUDANTES] NOCHECK CONSTRAINT [FK_AGEPROGQXAYUDANTES_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de profesional - Ayudante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autoumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'ID';

