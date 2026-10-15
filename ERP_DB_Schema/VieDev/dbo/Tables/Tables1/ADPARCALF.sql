CREATE TABLE [dbo].[ADPARCALF] (
    [CODCENATE] CHAR (10)    NOT NULL,
    [CODENTIDA] CHAR (9)     NOT NULL,
    [APLFESTR3] BIT          NOT NULL,
    [APLFESTR4] BIT          NOT NULL,
    [CONHORTRI] INT          NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADCALFEST] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [CODENTIDA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite establecer el parametro con el que se controla los parametros de apertura de Ingresos segun el calendario de no atenciones en TRIAGE III y IV.    1: Control Paciente  2: Registro TRIAGE    Nota: Este parametro tomara la hora segun la seleccion y la comparara contra el calendario para decidir si realiza apertura de ingreso o no.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CONHORTRI';

