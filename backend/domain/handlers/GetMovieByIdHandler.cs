using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Movies.Domain.DataAccess;
using MediatR;

namespace Movies.Domain.Handlers {
    public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdRequest, GetMovieByIdResponse> {

        private readonly IMoviesRepository _moviesRepository;
        public GetMovieByIdHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }
        
        public async Task<GetMovieByIdResponse> Handle(GetMovieByIdRequest request, CancellationToken cancellationToken) {
            var movie = await _moviesRepository.GetMovieByIdAsync(request.Id);

            return movie == null ? null : new GetMovieByIdResponse {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                PosterUri = movie.PosterUri

            };
        }
    }

    public class GetMovieByIdResponse {
        public Guid Id { get; set; }
        public string Title { get; set; }   
        public int Rating { get; set; }
        public string PosterUri { get; set; }
    }

    public class GetMovieByIdRequest : IRequest<GetMovieByIdResponse> {

        public GetMovieByIdRequest(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}