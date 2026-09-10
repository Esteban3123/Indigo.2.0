'************************************************************
' Assembly         : Infraestructure.Data.CommonRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 03-07-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base
Imports Infrastructure.Data.Base
Imports Domain.Entities
Public Class CommonHealthProfessionalRepository
    Inherits GenericRepository(Of HealthProfessional)
    Implements ICommonHealthProfessionalRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetHealthProfessionalByCode(identificationNumber As String) As HealthProfessional Implements ICommonHealthProfessionalRepository.GetHealthProfessionalByCode
        Dim query = (From x In _context.HealthProfessional
                     Where x.IdentificationNumber = identificationNumber
                     Select x)?.FirstOrDefault

        If query IsNot Nothing Then
            query.IdentificationTypeName = (From x In _context.ADTIPOIDENTIFICA.AsNoTracking() Where x.ID = query.IdentificationTypeId Select x)?.FirstOrDefault()?.NOMBRE
        End If
        Return If(query, New HealthProfessional)
    End Function
End Class
