CREATE TABLE [dbo].[HCREFRECDE] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECREGIS]      DATETIME      NOT NULL,
    [OBSERVACION]   VARCHAR (200) NULL,
    [FUNCREG]       CHAR (20)     NOT NULL,
    [ENVIAR]        BIT           NULL,
    [FECSISTEMA]    DATETIME      NULL,
    [MEDRECIB]      CHAR (20)     NULL,
    [HCMOTREFRECID] INT           NULL,
    [HCREFRECEPID]  INT           NOT NULL,
    [ESTADO]        NCHAR (10)    NULL,
    [ASUNTO]        VARCHAR (100) NULL,
    [MENSAJE]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCREFRECDE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFRECDE_HCMOTREFREC] FOREIGN KEY ([HCMOTREFRECID]) REFERENCES [dbo].[HCMOTREFREC] ([ID]),
    CONSTRAINT [FK_HCREFRECDE_HCREFRECDE] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCREFRECDE] ([ID]),
    CONSTRAINT [FK_HCREFRECDE_HCREFRECEP] FOREIGN KEY ([HCREFRECEPID]) REFERENCES [dbo].[HCREFRECEP] ([ID])
);


GO
ALTER TABLE [dbo].[HCREFRECDE] NOCHECK CONSTRAINT [FK_HCREFRECDE_HCMOTREFREC];


GO
ALTER TABLE [dbo].[HCREFRECDE] NOCHECK CONSTRAINT [FK_HCREFRECDE_HCREFRECDE];


GO
ALTER TABLE [dbo].[HCREFRECDE] NOCHECK CONSTRAINT [FK_HCREFRECDE_HCREFRECEP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Aceptado  2- No Aceptado  3- Sin Definir Conducta  4- Cancelado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-> Debe enviar correo    0-> No debe enviar correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ENVIAR';

