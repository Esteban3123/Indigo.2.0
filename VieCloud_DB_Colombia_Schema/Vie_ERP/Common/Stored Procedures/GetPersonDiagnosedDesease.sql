-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonDiagnosedDesease]  
-- Add the parameters for the stored procedure here  
@IdentificationNumber VARCHAR(20)
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;
        SELECT [Common].[PersonDiagnosedDisease].[Id], 
               [Payroll].[DiagnosedDisease].[Name] AS DiagnosedDiseaseName, 
               [Payroll].[DiagnosedDisease].[Code]
        FROM [Common].[PersonDiagnosedDisease]
             INNER JOIN [Common].[Person] ON [Common].[Person].[Id] = [Common].[PersonDiagnosedDisease].[PersonId]
             INNER JOIN [Payroll].[DiagnosedDisease] ON [Common].[PersonDiagnosedDisease].[DiagnosedDiseaseId] = [Payroll].[DiagnosedDisease].[Id]
        WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las enfermedades o patologías diagnosticadas registradas para una persona, buscándola por su número de identificación (cédula o documento). Combina el registro maestro de personas con el catálogo de enfermedades diagnosticadas del módulo de nómina, devolviendo el identificador del registro, el nombre de la enfermedad y su código. Se usa para conocer el historial de diagnósticos asociados a un empleado o paciente, especialmente en procesos de incapacidad, licencias médicas o ausentismo laboral.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonDiagnosedDesease';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonDiagnosedDesease';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene las enfermedades diagnosticadas registradas para una persona identificada por su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.Person con el IdentificationNumber suministrado; Deben existir registros en Common.PersonDiagnosedDisease asociados a la persona; El DiagnosedDiseaseId referenciado debe existir en Payroll.DiagnosedDisease', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan diagnósticos cuya enfermedad existe en el catálogo Payroll.DiagnosedDisease (INNER JOIN); Solo se retornan diagnósticos cuya persona existe en Common.Person (INNER JOIN); El filtro se realiza exclusivamente por número de identificación de la persona', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Enfermedad diagnosticada; Diagnóstico; Número de identificación; Catálogo de enfermedades', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonDiagnosedDisease: Cuando Common.Person.IdentificationNumber coincide con el parámetro, retorna Id de PersonDiagnosedDisease junto al nombre y código de la enfermedad diagnosticada', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonDiagnosedDisease; Common.Person; Payroll.DiagnosedDisease', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDiagnosedDesease';
-- GO
