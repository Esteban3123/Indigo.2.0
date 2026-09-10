'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 03-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class HealthProfessionalRepository
    Inherits GenericRepository(Of INPROFSAL)
    Implements IHealthProfessionalRepository


    ' contexto del repositorio de ciudades
    Private _context As ICrystalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As ICrystalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un profesional de la salud por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthProfessionalByCode(code As String) As INPROFSAL Implements IHealthProfessionalRepository.GetHealthProfessionalByCode
        Dim query = From e In _context.INPROFSAL.Include("INESPECIA").Include("INESPECIA1").Include("INESPECIA2")
                    Where e.CODPROSAL = code
                    Select e
        If query.Count > 0 Then
            Return query.SingleOrDefault()
        Else
            Return New INPROFSAL()
        End If
    End Function

    

End Class
