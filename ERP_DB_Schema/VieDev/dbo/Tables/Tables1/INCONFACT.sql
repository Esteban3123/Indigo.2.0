CREATE TABLE [dbo].[INCONFACT] (
    [CODCONCEP]  CHAR (3)      NOT NULL,
    [DESCONCEP]  VARCHAR (100) NOT NULL,
    [RIPSCONCEP] CHAR (2)      NOT NULL,
    [INFOCONCE]  CHAR (2)      NOT NULL,
    CONSTRAINT [PK_INCONFACT] PRIMARY KEY CLUSTERED ([CODCONCEP] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Informe de consolidacion  1=Medicamentos;  2=Cirugias Procedimientos  3=Imagenes Diagnosticas  4=Laboratorios  5=Terapias  6=Consultas  7=Bancos de Sangre  8=Elementos adicionales  9=Habitacion  10=Honorarios Medicos  11=Atencion y sala de observacion Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'INFOCONCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rips  1=Consultas;  2=Procedimientos Diagnosticos;  3=Procedimientos Terapeuticos NO Quirurgicos;  4=Procedimientos Terapeuticos Quirurgicos;  5=Procedimientos Promocion Prevencion;  6=Estancias;  7=Honorarios;  8=Derecho de Sala;  9=Materiales e Insumos;  10=Bancos de Sangre;  11=Protesis y Ortesis;  12=Medicamentos POS;  13=Medicamentos No POS;  14=Traslado de Pacientes;  15=Otros Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'RIPSCONCEP';

