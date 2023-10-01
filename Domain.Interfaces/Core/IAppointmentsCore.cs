﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAppointmentsCore
    {
        Task<GenericResponseModel> GetAppointmentsList(string? filter = "");
        Task<GenericResponseModel> SetAppointment(AppointmentesModel Item);
    }
}