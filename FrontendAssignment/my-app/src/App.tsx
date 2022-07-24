import axios from 'axios';
import { useState } from 'react';
import './App.css';
import IconButton from '@mui/material/IconButton';
import TextField from "@mui/material/TextField";
import SearchIcon from "@mui/icons-material/Search";
import { Box, Button, Grid, Paper, Skeleton } from "@mui/material";

function App() {
  const [characterName, setCharacterName] = useState("");
  const [characterInfo, setCharacterInfo] = useState<null | undefined | object>(undefined);

  const DRAGONBALL_BASE_URL = "https://dragon-ball-api.herokuapp.com/api";
  return (
    <div>
      <h1 style={{ marginLeft: "2%" }}>Dragon Ball Character Search</h1>
      <div style={{ display: "flex", justifyContent: "center", marginTop: "5%" }}>
        <TextField
          id="search-box"
          className="text"
          value={characterName}
          label="Enter a character name"
          variant="outlined"
          onChange={(prop: any) => {
            setCharacterName(prop.target.value);
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

      {characterInfo === undefined ? (
        <div></div>
      ) : (
        <div id="character-result"
          style={{
            maxWidth: "800px",
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
                  {characterInfo === undefined || characterInfo === null ? (
                    <h1>Character Not Found</h1>
                  ) : (
                    <div>
                      <h1></h1>
                    </div>
                  )}
                </Box>
              </Grid>
            </Grid>
          </Paper>
        </div>
      )}
    </div>
  )

  function search() {
    if (characterName === undefined || characterName === "") {
      return;
    }
    axios.get(DRAGONBALL_BASE_URL + "/character/" + characterName.toLowerCase()).then((res) => {
      setCharacterInfo(res.data);
    }).catch(() => {
      setCharacterInfo(null);
    });
  }


}

export default App;
