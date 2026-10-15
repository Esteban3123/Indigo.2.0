CREATE TABLE [dbo].[HCFAVORTI] (
    [OPCIONFAV]                CHAR (10) NOT NULL,
    [CODCATEGO]                CHAR (1)  NOT NULL,
    [CODIGITEM]                CHAR (20) NOT NULL,
    [PRIORIDAD]                INT       NULL,
    [TIPFAVORTI]               CHAR (1)  NOT NULL,
    [ROL]                      INT       CONSTRAINT [DF__HCFAVORTI__ROL__1DE1AD90] DEFAULT ((0)) NOT NULL,
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    [USUARIOCREACION]          CHAR (20) NULL,
    [FECHACREACION]            DATETIME  NULL,
    CONSTRAINT [PK_HCFAVORTI_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFAVORTI_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCFAVORTI] NOCHECK CONSTRAINT [FK_HCFAVORTI_SEGusuaru_1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ROL:   Otras categorias diferentes de Insumos  0=por defecto (nulo)  Categoria Insumos:  0= lista vacia   1=Medico     2=Enfermeria     3=Terapeuta  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'ROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Categoria:  1. Medicamentos  2. Labaratorios  3. Patologias  4. Imagenes DX  5. Procedimientos QX  6. Procedimientos No Qx  7. Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'CODCATEGO';

