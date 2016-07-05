using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IMovieHandler
{
    void Stop();
    void Play();
    void Pause();
    bool Loop { get; set; }
}

