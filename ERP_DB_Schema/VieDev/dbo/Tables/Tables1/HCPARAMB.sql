CREATE TABLE [dbo].[HCPARAMB] (
    [ID]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]           CHAR (20)       NOT NULL,
    [ANALITO]             VARCHAR (200)   NULL,
    [RESULTADO]           TINYINT         NOT NULL,
    [OBLIGATORIO]         INT             NOT NULL,
    [LONGITUD]            INT             NULL,
    [VALMIN]              NUMERIC (18, 2) NULL,
    [VALMAX]              NUMERIC (18, 2) NULL,
    [PERMITEDECI]         INT             NULL,
    [CLASIFICACION]       VARCHAR (200)   NULL,
    [ESTADO]              INT             NOT NULL,
    [TIPOPARACLI]         INT             NULL,
    [USUARIOCREACION]     CHAR (20)       NULL,
    [FECHACREACION]       DATETIME        NULL,
    [USUARIOMODIFICACION] CHAR (20)       NULL,
    [FECHAMODIFICACION]   DATETIME        NULL,
    CONSTRAINT [PK_HCPARAMB] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARAMB_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARAMB_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARAMB_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCPARAMB] NOCHECK CONSTRAINT [FK_HCPARAMB_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCPARAMB] NOCHECK CONSTRAINT [FK_HCPARAMB_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARAMB] NOCHECK CONSTRAINT [FK_HCPARAMB_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo paraclinico  1->Laboratorio  2->Imagenes Dx  3->Patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'TIPOPARACLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si el analito es obligatorio:   1-> si,  2-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado que puede almacenar dico analito  1-> Valor Predefinido  2-> Campo Texto  3-> Campo Numérico  cuando se selecciona valor predefinido se deben cargar los datos de la tabla HCVALPARACL y almacenar dicha relación en otra tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'RESULTADO';

