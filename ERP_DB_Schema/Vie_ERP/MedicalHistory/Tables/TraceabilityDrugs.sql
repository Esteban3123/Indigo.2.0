CREATE TABLE [MedicalHistory].[TraceabilityDrugs] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [ProductCode]         CHAR (20)     NOT NULL,
    [NUMINGRES]           CHAR (10)     NOT NULL,
    [ProfessionalCode]    CHAR (20)     NOT NULL,
    [RegistrationDate]    DATETIME      NOT NULL,
    [Action]              INT           NOT NULL,
    [UFUCODIGO]           CHAR (10)     NOT NULL,
    [AdministrationRoute] VARCHAR (250) NULL,
    [Justification]       VARCHAR (250) NULL,
    [IdSourceTable]       INT           NOT NULL,
    [SourceTable]         VARCHAR (50)  NOT NULL,
    [ProccesType]         TINYINT       CONSTRAINT [DF_TraceabilityDrugs_ProccesType] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TraceabilityDrugs] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Precipcion de medicamentos
2 - Mezclas y liquidos', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProccesType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso que realiza la acción:    1 - Enfermeria hoja de medicamento - Programacion Medicamento.  2 - Enfermeria hoja de medicamento - Adicion de medicamento Medicamento.  3 - Enfermeria hoja de medicamento - Modificacion de medicamento.  4 -  Enfermeria hoja de medicamento - Aplicacion de Medicamento.  5 -  Enfermeria hoja de medicamento - Eliminar Medicamento.  6 -  Enfermeria hoja de medicamento - NO Aplicado de Medicamento.  7 -  Enfermeria hoja de medicamento - Dosis Adicional Medicamento.  8 -  Enfermeria hoja de medicamento - Suspender Aplicaciones.  9 -  Enfermeria hoja de medicamento - Descartar sobrante  10 -  Enfermeria hoja de medicamento - Aplicacion segun necesidad.  11 - Enfermeria solicitud medicamento insumos - Solicitud de enfermeria.  12- Gestion hospitalaria - Traslado de medicamento  13- Gestion hospitalaria - Aceptacion de traslado de medicamento.  14- Gestion hospitalaria - Rechazo de  traslado de medicamento.  15- Gestion hospitalaria - Devolutivo de medicamento.  16 - Farmacia - Entrega de medicamento.  17 - Farmacia - Anulacion de solicitud de medicamento.  18 - Farmacia - Aceptacion de devolutivo.  19 - Farmacia - Anulacion de devolutivo.  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro del movimiento realizado', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo profesional que efectua la accion ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Ingreso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Producto ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Id';

