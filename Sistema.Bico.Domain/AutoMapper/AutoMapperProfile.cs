using AutoMapper;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Response;
using System;
using System.Linq;

namespace Sistema.Bico.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        private string _imageDefault = "/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0MiA3OS4xNjA5MjQsIDIwMTcvMDcvMTMtMDE6MDY6MzkgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE4IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDo2M0U3NTM2MkQ0NEUxMUU4Qjk0OEE5MzM2RDU3RENEMiIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDo2M0U3NTM2M0Q0NEUxMUU4Qjk0OEE5MzM2RDU3RENEMiI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOjYzRTc1MzYwRDQ0RTExRThCOTQ4QTkzMzZENTdEQ0QyIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOjYzRTc1MzYxRDQ0RTExRThCOTQ4QTkzMzZENTdEQ0QyIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQICAgICAgICAgICAwMDAwMDAwMDAwEBAQEBAQECAQECAgIBAgIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD/8AAEQgAZABkAwERAAIRAQMRAf/EAaIAAAAGAgMBAAAAAAAAAAAAAAcIBgUECQMKAgEACwEAAAYDAQEBAAAAAAAAAAAABgUEAwcCCAEJAAoLEAACAQMEAQMDAgMDAwIGCXUBAgMEEQUSBiEHEyIACDEUQTIjFQlRQhZhJDMXUnGBGGKRJUOhsfAmNHIKGcHRNSfhUzaC8ZKiRFRzRUY3R2MoVVZXGrLC0uLyZIN0k4Rlo7PD0+MpOGbzdSo5OkhJSlhZWmdoaWp2d3h5eoWGh4iJipSVlpeYmZqkpaanqKmqtLW2t7i5usTFxsfIycrU1dbX2Nna5OXm5+jp6vT19vf4+foRAAIBAwIEBAMFBAQEBgYFbQECAxEEIRIFMQYAIhNBUQcyYRRxCEKBI5EVUqFiFjMJsSTB0UNy8BfhgjQlklMYY0TxorImNRlUNkVkJwpzg5NGdMLS4vJVZXVWN4SFo7PD0+PzKRqUpLTE1OT0laW1xdXl9ShHV2Y4doaWprbG1ub2Z3eHl6e3x9fn90hYaHiImKi4yNjo+DlJWWl5iZmpucnZ6fkqOkpaanqKmqq6ytrq+v/aAAwDAQACEQMRAD8A29/Yp6Keve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917oMe0u1dudU4IZXNs9TW1Zkhw+GpmUVmTqY0DMFLXWnpIdS+WZgVQMAAzMqtdVLGg62BXqujd3ya7W3RVStR5xtrY4uTBjtvotM0SfRRLkmVsjPJb9R8ioTyEX6e3xGo+fVwo6SeM7z7cxNQlTT7/ANxzsj6/Hk658xTtzcq9PlBWQsh/pbj8W970IfLrdB0dHpT5QUu866k2rviClxO4atkp8ZlqXVFisvUEWSlnikZv4dXzEWT1GKVzpGhiqs08dMrw6oVpkdHA9s9V697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3XuqdO8d81W/eyNw5KSZpMdjqyfCYOK58UOLxs8sETxqSQGrZQ87n8tJ/QABWi6V6dAoOgi93631737r3XJHaNldGZHRg6OhKsjKQVZWBBVlIuCPp791rq4noze1Rv7rPb2cr5PLlYY5sRl5P7UtfjJPt2qH5P7lZTCOZvxqkNvaRxpanl02RQ9C77p1rr3v3Xuve/de697917r3v3Xuve/de697917ro8gi5Fx9R9R/iP8ffuvdUW52jqMdm8xj6tWWqocpkKOpVxZxPTVcsMwcf6oSIb+1o4dO9NXvfW+ve/de697917q0T4h0VRS9TPPMHEeR3RmKyk1CytTpBjqBnT8lTU0Ug/wBce00vxfl023Ho0ntrqvXvfuvde9+691737r3Xvfuvde9+691737r3XvfuvdVwfKjp6uw+equx8FSPUYLNyLLuCOnjd2xGXICS1s6qDpocoQHMn0WoLBra0uoieo0nj1dT5dE29vdX697917pZ7C2Jn+xdx0W3Nv0zSz1Dq1XWMjmjxdEGAnr6+VQRFBCv0H6pGsigsQPdWYKKnrRNOrk9pbZx2zttYXa+KUigwtDDRQswAkmZAXnqZbXHmq6h3le3Gtz7SE1NT02c9KL3rrXXvfuvde9+691737r3Xvfuvde9+691737r3THuDcu39q4+TK7jzFBhsfHe9TX1CQK7AX8UKMfJUTN+EjDO34B97AJwOvdAHUfKjpmor1ws1ZlKugrddJU5KXBSthFilvE61kdSyV0lNIpIa1M66Tzxf254T8eraT0y5z4w9Rb9jXcGz8nNhYK8iaObbVZSZLAzalBYw0kvmSD630QyxohNtI+nvwkZcHr2ojpjxfwu2hT1KS5fd2fydMrampaSkocYZB+EedjkHC/10hSfwR72Zj5Dr2o9LKo7L6H6Eij2vhUj+5Z0/iNLtiCLMZBZI1CCozuSnq4hJUKCf23meVAeIwtvetLvk9eoTnoTdldx9c9gSCm23uSlmyJF/wCFVqS43Jm318NJWpC1UF/JhMgH5PurIy8etEEdCf7p1rr3v3Xuve/de697917r3v3Xuve/de6DntLsjEdX7Tq9x5MCoqC32eHxqvplyeUljkeCmDcmOBFjLzSWOiNSQC2lTZVLGg62BU9VG7335ufsLNS53dGRkraptSU0C3jocdTFiy0mPpQxjpqdP8Ls59TszEkqlUKKDpwCnDpH+7db6esPuPcG3pGmwOcy+FlY3d8XkaugLkcfufazReTgfm/vRAPHrXT1k+x+wM1AabK713TX0zLpenqc7kpIHW1tMkRqPHILf1B96CqOAHXqDpF+7db65xSyQSRzQyPDNE6yRSxO0ckUiMGSSN0IZHRgCCCCD791rqxX429/1e6pothb3rPuM8I2O3s1NYS5eKniMkuPr34EmShijLxynmdAQ37gBkTyJTuHDqjDzHR0PbPVeve/de697917r3v3Xuve/de6Jr8vthZ7P4LDbuxUlTWUO1UrI8tiYwziCkrWids3FGvLfbmAJPwSsZVuFVz7eiYA0Pn1ZT1XD7UdOde9+691737r3Xvfuvde9+691737r3Q29B9fZ3fXYGHkxklTj8ftyuos1l83AGX7CKjnWenghltp++r5YfHEvPGpyCqN7o7BVz1Umg6t79pOm+ve/de697917r3v3Xuve/de64OiSo8UqLJHIrJJG6h0dHBV0dGBVlZTYg8Ee/de6r17t+LlfQVNXujrOjevxkzSVNftWGzVuOdjrkfCxmzVtCbkinF5ovogdbBFCSVw3Hq4b16JRLFLBLJDPHJDNC7RyxSo0csUiMVeOSNwHR0YWIIBB9vdW6x+/db697917r3v3Xuhj6t6R3l2jVwyY+kfGbcWYJXblromShjRWHljoUJR8nWAcCOP0q1tboDf3RnC8ePVSQOrUNg7C291xt2m25t2mMVNETNVVc2l63JVrqomrq6ZVXyTSaQAAAqIAqgKAPaZmLGp6oTU16WvuvWuve/de697917r3v3Xuve/de697917r3v3Xug03r1D152AGfcm3KOauYC2Wo9WPyykfTVXUhilqFH+pl8if4e7BmXh1sEjovWV+F20aiZ3w+78/jYW5WCtpKDK+Mn8CSL+GMyj8Xuf8fbnjHzHW9R6a6b4T4pGvWdg5CdL8rTbepqRtN/pqlytaL2/Nv8AYe9+MfTreroXtp/GDqfa00NXLiqrctbCAVm3JUrW04kBB1jGwQ0uOfkcCSOS3+vz7oZGPWixPRgYIIKaGOnpoYqenhRY4YII0ihijUWVI4owqIij6AAAe6dV6y+9de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuve/de697917r3v3Xuv/9k=";
        public AutoMapperProfile()
        {
            _ = CreateMap<Client, AddClientCommand>()
               .ReverseMap();

            _ = CreateMap<Worker, AddWorkerCommand>()
               .ReverseMap();


            _ = CreateMap<ProfessionalProfile, AddProfessionalCommand>()
               .ReverseMap();


            _ = CreateMap<ThreeAvaliation, ThreeAvaliationResponse>()
               .ReverseMap();

            _ = CreateMap<WorkerDone, WorkerDoneResponse>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToString("dd/MM/yyyy")))
                .ReverseMap();

            _ = CreateMap<ApplicationUser, AddClientCommand>()
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ReverseMap()
               .AfterMap((src, dest) => dest.PhoneNumberConfirmed = true)
               .AfterMap((src, dest) => dest.EmailConfirmed = true);

            _ = CreateMap<ProfessionalProfile, ProfessionalProfileResponse>()
                .ForMember(dst => dst.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dst => dst.LastName, map => map.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Phone, map => map.MapFrom(src => src.Phone))
                .ForMember(dst => dst.Perfil, map => map.MapFrom(src => src.Perfil))
                .ForMember(dst => dst.Avaliation, map => map.MapFrom(src => src.Avaliation == null ? "0,0" : src.Avaliation.ToString()))
                .ForMember(dst => dst.PerfilPicture, map => map.MapFrom(src => src.PerfilPicture.Length > 0 ? Convert.ToBase64String(src.PerfilPicture) : _imageDefault))
                .ForMember(dst => dst.Especiality, map => map.MapFrom(src => src.Especiality.ToList().Select(s => s.Description)))
                .ForMember(dst => dst.Area, map => map.MapFrom(src => src.ProfessionalArea.Codigo))
                .ForMember(dst => dst.Sobre, map => map.MapFrom(src => src.About))
                .ForMember(dst => dst.Profession, map => map.MapFrom(src => src.Profession))
                .ForMember(dst => dst.Logradouro, map => map.MapFrom(src => src.Address.Logradouro))
                .ForMember(dst => dst.Number, map => map.MapFrom(src => src.Address.Number))
                .ForMember(dst => dst.Bairro, map => map.MapFrom(src => src.Address.Bairro))
                .ForMember(dst => dst.State, map => map.MapFrom(src => src.Address.State))
                .ForMember(dst => dst.Complemento, map => map.MapFrom(src => src.Address.Complemento))
                .ForMember(dst => dst.CEP, map => map.MapFrom(src => src.Address.ZipCode))
                .ForMember(dst => dst.City, map => map.MapFrom(src => src.Address.City))
                .ForMember(dst => dst.Email, map => map.MapFrom(src => src.Client.Email))
                .ForMember(dst => dst.Ativo, map => map.MapFrom(src => src.Ativo))
                .ForMember(dst => dst.Premium, map => map.MapFrom(src => src.Premium));

            _ = CreateMap<ProfessionalClient, ProfessionalClientResponse>()
                 .ForMember(dst => dst.ProfessionalProfile, map => map.MapFrom(src => src.ProfessionalProfile))
                 .ForMember(dst => dst.Client, map => map.MapFrom(src => src.Client))
                 .ForMember(dst => dst.StatusWorker, map => map.MapFrom(src => src.StatusWorker));

            _ = CreateMap<Client, ClientResponse>()
                   .ForMember(dst => dst.Name, map => map.MapFrom(src => src.Name))
                   .ForMember(dst => dst.LastName, map => map.MapFrom(src => src.LastName))
                   .ForMember(dst => dst.PhoneNumber, map => map.MapFrom(src => src.ApplicationUser.FirstOrDefault().PhoneNumber))
                   .ForMember(dst => dst.PerfilPicture, map => map.MapFrom(src => src.PerfilPicture != null && src.PerfilPicture.Length > 0 ? Convert.ToBase64String(src.PerfilPicture) : _imageDefault));


            _ = CreateMap<Worker, WorkerResponse>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dst => dst.Created, map => map.MapFrom(src => src.Created))
                .ForMember(dst => dst.Price, map => map.MapFrom(src => src.Price))
                .ForMember(dst => dst.Titulo, map => map.MapFrom(src => src.Title))
                .ForMember(dst => dst.Phone, map => map.MapFrom(src => src.Phone))
                .ForMember(dst => dst.Profession, map => map.MapFrom(src => src.Profession))
                .ForMember(dst => dst.Area, map => map.MapFrom(src => src.ProfessionalArea.Codigo))
                .ForMember(dst => dst.Sobre, map => map.MapFrom(src => src.About))
                .ForMember(dst => dst.State, map => map.MapFrom(src => src.Address.State))
                .ForMember(dst => dst.CEP, map => map.MapFrom(src => src.Address.ZipCode))
                .ForMember(dst => dst.City, map => map.MapFrom(src => src.Address.City))
                .ForMember(dst => dst.Interessados, map => map.MapFrom(src => src.WorkerProfessional == null ? 0 : src.WorkerProfessional.Count));

            _ = CreateMap<ApplicationUser, ClientResponse>()
                .ForMember(dst => dst.Name, map => map.MapFrom(src => src.Client.Name))
                .ForMember(dst => dst.LastName, map => map.MapFrom(src => src.Client.LastName))
                .ForMember(dst => dst.CpfCnpj, map => map.MapFrom(src => src.Client.CpfCnpj))
                .ForMember(dst => dst.PhoneNumber, map => map.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.PerfilPicture, map => map.MapFrom(src => src.Client.PerfilPicture != null && src.Client.PerfilPicture.Length > 0 ? Convert.ToBase64String(src.Client.PerfilPicture) : _imageDefault));

        }
    }
}