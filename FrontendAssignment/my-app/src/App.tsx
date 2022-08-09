import axios from 'axios';
import { useState } from 'react';
import './App.css';
import IconButton from '@mui/material/IconButton';
import TextField from "@mui/material/TextField";
import SearchIcon from "@mui/icons-material/Search";
import { Box, Button, Grid, Paper, Skeleton } from "@mui/material";
import { CurrencyBitcoin } from '@mui/icons-material';
import { forEachChild } from 'typescript';

function App() {
  const [country, setCountry] = useState("");
  const [countryInfo, setCountryInfo] = useState<null | undefined | any>(undefined);

  const COUNTRIES_BASE_URL = "https://restcountries.com/v2";
  return (
    <div>
      <h1 style={{ marginLeft: "2%" }}>Country Search</h1>
      <div style={{ display: "flex", justifyContent: "center", marginTop: "5%" }}>
        <TextField
          id="search-box"
          className="text"
          value={country}
          label="Enter a country"
          variant="outlined"
          onChange={(prop: any) => {
            setCountry(prop.target.value);
          }}
          placeholder="Search..."
          size="small" />
        <IconButton
          aria-label="search"
          onClick={() => {
            search();
          }}>
          <SearchIcon style={{ fill: "blue" }} />
        </IconButton>
      </div>

      {countryInfo === undefined ? (
        <div></div>
      ) : (
        <div id="country-result"
          style={{
            maxWidth: "600px",
            margin: "0 auto",
            padding: "100px 10px 0px 10px"
          }}>
          <Paper elevation={3} variant="outlined">
            <Grid
              container
              justifyContent="center"
              alignItems="center">
              <Grid item>
                <Box>
                  <h2>{countryInfo.name}</h2>
                  <img height="150px"
                    style={{ border: "1pt solid rgba(0,0,0,0.1)" }}
                    src={countryInfo.flags.png}
                    alt={countryInfo.name.common} />
                </Box>
                <Grid item>
                  <Box>
                    {countryInfo === undefined || countryInfo === null ? (
                      <h2>Country Not Found</h2>
                    ) : (
                      <div>
                        <p>
                          Name: {countryInfo.name}
                          <br />
                          Region: {countryInfo.region}
                          <br />
                          Capital: {countryInfo.capital}
                          <br />
                          Population: {countryInfo.population}
                          <br />
                          Language(s): {getLanguages().toString().replace(",", ", ")}
                          <br />
                          Currency: {countryInfo.currencies[0].name}, {countryInfo.currencies[0].symbol}
                        </p>
                      </div>
                    )}
                  </Box>
                </Grid>

              </Grid>
            </Grid>
          </Paper>
        </div>
      )}
    </div>
  )

  function search() {
    if (country === undefined || country === "") {
      return;
    }
    axios.get(COUNTRIES_BASE_URL + "/name/" + country + "?fullText=true").then((res) => {
      console.log(res.data[0]);
      setCountryInfo(res.data[0]);
    }).catch(() => {
      setCountryInfo(null);
    });

  }

  function getLanguages() {
    const languages: String[] = [];
    if (countryInfo !== undefined && countryInfo !== null) {
      countryInfo.languages.forEach((language: any) => languages.push(language.name))
    }
    return languages;
  }

}

export default App;
