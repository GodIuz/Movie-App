﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoApp6.Data;
using DemoApp6.Models;

namespace DemoApp6.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DemoApp6Context _context;

        public MoviesController(DemoApp6Context context)
        {
            _context = context;
        }

        // GET: Movies

        public async Task<IActionResult> Index(String movieGenre,string SearchString)
        {
            //var movies = from m in _context.Movies
            //             select m;
            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    movies = movies.Where(s => s.Title.Contains(SearchString));
            //}
            //return View(await movies.ToListAsync());

            //IQueryable<string> genreQuery = from m in _context.Movies
            //                                orderby m.Genre
            //                                select m.Genre;

            IQueryable<string> genreQuery = _context.Movies.Select(s => s.Genre).OrderBy(x => x);

            var movies = from m in _context.Movies
                         select m;

            if(!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }
            if(!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            var moviesGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };
            return View(moviesGenreVM);
        }

        //public IActionResult Index(String movieGenre, string SearchString)
        //{
        //    IQueryable<string> genreQuery = _context.Movies.Select(s => s.Genre).OrderBy(x => x);

        //    var movies = from m in _context.Movies
        //                 select m;

        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        movies = movies.Where(s => s.Title.Contains(SearchString));
        //    }
        //    if (!string.IsNullOrEmpty(movieGenre))
        //    {
        //        movies = movies.Where(x => x.Genre == movieGenre);
        //    }
        //    var moviesGenreVM = new MovieGenreViewModel
        //    {
        //        Genres = new SelectList(genreQuery.Distinct().ToList()),
        //        Movies = movies.ToList()
        //    };
        //    return View(moviesGenreVM);
        //}

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movies movies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movies movies)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
